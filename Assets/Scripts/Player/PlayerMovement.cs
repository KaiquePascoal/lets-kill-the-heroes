using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float maxJumpChargeTime = 1f;
    public float minJumpForce = 5f;
    public float maxJumpForce = 15f;
    private float jumpChargeTimer = 0f;
    private bool isChargingJump = false;


    [Header("Floor Verification")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Animation")]
    public Animator playerAnimator;

    [Header("Knockback Settings")]
    private bool isKnockedback = false;
    [SerializeField] private float _knockbackDuration = 0.3f;
    [SerializeField] private float _knockbackTimer = 0f;

    [Header("Attack ")]
    public BoxCollider2D triggerAttack;
    public float cdrAttack = .6f;
    private bool _canAttack = true;

    private Rigidbody2D rb;
    private InputSystem_Actions inputActions;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool isGrounded;
    private bool wasGroundedLastFrame;
    private bool isJumpHeld = false;
    private bool isSFXFallPlayed = true;
    private SpriteRenderer _spriteRender;

    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isOnEffectArea = false;


    [Header("VARIÁVEIS FÍSICA")]
    [SerializeField] private GameObject floor;
    [SerializeField] private float raioDesse, raioOutro, massDesse, massOutro, limite;
    [SerializeField] private Vector3 velocidadeDesse, velocidadeOutro, velocidade, aceleration;
    private bool isColliding = false;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions(); // Usa o asset padrão
        triggerAttack.enabled = false;
        _spriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;

        inputActions.Player.Jump.started += OnJumpStarted;
        inputActions.Player.Jump.canceled += OnJumpCanceled;

        inputActions.Player.Interact.started += OnKey;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;

        inputActions.Player.Jump.started -= OnJumpStarted;
        inputActions.Player.Jump.canceled -= OnJumpCanceled;

        inputActions.Player.Interact.started -= OnKey;

        inputActions.Disable();
    }

    [System.Obsolete]
    void Update()
    {
        if (isDead) return;

        // Se o personagem está no ar, dentro da área de efeito, e subindo
        if (!isGrounded && isOnEffectArea && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            Debug.Log("Pulo cancelado!");
        }


        if (gameObject.transform.lossyScale.x <= .7f)
        {
            GameManager.Instance.InstaDeath();

        }

        bool isCurrentlyGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (!wasGroundedLastFrame && isCurrentlyGrounded)
        {
            playerAnimator.SetTrigger("Land");
        }

        isGrounded = isCurrentlyGrounded;
        wasGroundedLastFrame = isCurrentlyGrounded;

        if (isChargingJump)
        {
            jumpChargeTimer += Time.deltaTime;
        }

        playerAnimator.SetBool("isJumping", !isGrounded);

        if (!isGrounded)
        {
            playerAnimator.SetBool("isFalling", rb.velocity.y < -0.1f);
            isSFXFallPlayed = false;
        }
        else
        {
            playerAnimator.SetBool("isFalling", false);
            if (!isSFXFallPlayed)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.fall);
                isSFXFallPlayed = true;
            }
        }
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        if (isDead) return;

        if (isOnEffectArea)
        {
            Vector3 distanciaFloor = floor.transform.position;
            Vector3 distanciaPlayer = transform.position;
            float distance = Vector3.Distance(distanciaFloor, distanciaPlayer);

            if (distance <= (raioDesse + raioOutro))
            {
                if (!isColliding)
                {
                    isColliding = true;
                    velocidadeDesse = (massOutro * 2 / (massDesse + massOutro)) * velocidadeOutro;
                    velocidade = velocidadeDesse;

                    Debug.Log("Objetos colidindo!");
                }
            }

            velocidade += aceleration * Time.deltaTime;
            if (velocidade.y > limite) velocidade.y = limite;

            transform.position += velocidade * Time.deltaTime + (aceleration * (Time.deltaTime * Time.deltaTime)) / 2f;
        }


        if (isKnockedback)
        {
            _knockbackTimer -= Time.fixedDeltaTime;
            if (_knockbackTimer <= 0f)
            {
                isKnockedback = false;
            }
            return; // NÃO aplica movimentação enquanto estiver em knockback
        }

        Vector2 velocity = rb.velocity;

        if (!isJumpHeld)
        {
            // Só aplica movimento horizontal se o botão de pulo NÃO estiver segurado
            velocity.x = moveInput.x * moveSpeed;
        }
        else
        {
            // Paralisa movimento horizontal enquanto o botão estiver segurado
            velocity.x = 0;
        }

        rb.velocity = new Vector2(velocity.x, rb.velocity.y);


        playerAnimator.SetFloat("SpeedX", moveInput.x);

        // Vira o personagem visualmente
        if (moveInput.x > 0.01f)
            transform.rotation = Quaternion.Euler(0, 0, 0); // olhando pra direita
        else if (moveInput.x < -0.01f)
            transform.rotation = Quaternion.Euler(0, 180, 0); // olhando pra esquerda
    }

    // Callback de movimento
    void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Callback de pulo
    [System.Obsolete]
    void OnJumpCanceled(InputAction.CallbackContext context)
    {
        if (isChargingJump && isGrounded)
        {
            float chargePercent = Mathf.Clamp01(jumpChargeTimer / maxJumpChargeTime);
            float finalJumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, chargePercent);

            rb.velocity = new Vector2(rb.velocity.x, finalJumpForce);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);

            isChargingJump = false;
            isJumpHeld = false;
            playerAnimator.SetBool("isPreparingJump", false);
        }
    }

    void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (!isOnEffectArea)
        {
            isJumpHeld = true;
            isChargingJump = true;
            jumpChargeTimer = 0f;
            playerAnimator.SetBool("isPreparingJump", true);
            //AudioManager.Instance.PlaySFX(AudioManager.Instance.prepareJump);
        }
    }

    void OnKey(InputAction.CallbackContext context)
    {
        if (!isOnEffectArea)
        {
            if (_canAttack && isGrounded)
            {
                triggerAttack.enabled = true;
                _canAttack = false;
                playerAnimator.SetTrigger("attack");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.grapeAttack);
                StartCoroutine(AttackCDR());
            }
        }
        
    }

    IEnumerator AttackCDR()
    {
        yield return new WaitForSeconds(.2f);
        triggerAttack.enabled = false;
        yield return new WaitForSeconds(cdrAttack - .2f);
        _canAttack = true;
    }

    [System.Obsolete]
    public void ApplyKnockback(Vector2 direction, float force, float duration)
    {
        isKnockedback = true;
        _knockbackTimer = duration;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hurt);

        rb.velocity = Vector2.zero;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
