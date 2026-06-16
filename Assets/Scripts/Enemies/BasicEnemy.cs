using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [Header("Scriptable")]
    [SerializeField] private EnemyBase _enemyScriptable;

    [Header("Attack")]
    public GameObject hitbox;
    public GameObject dangerZone;

    [Header("Knockback Settings")]
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _knockbackDuration = 0.2f;

    private Rigidbody2D _rb;
    private bool _isKnocked;
    private SpriteRenderer _spriteRenderer;
    private bool _isAlive = true;
    private Animator _animator;
    private AIChase _chase;
    private BoxCollider2D _boxCollider;
    private float _hp;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _chase = GetComponent<AIChase>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _hp = _enemyScriptable.hp;
    }

    private void Update()
    {
        if (!_isAlive)
        {
            _chase.enabled = false;
            dangerZone.SetActive(false);
            _rb.bodyType = RigidbodyType2D.Static;
            _boxCollider.enabled = false;
            _spriteRenderer.color = Color.grey;
        }
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.playerAnimator.SetTrigger("takeDamage");
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                player.ApplyKnockback(knockbackDirection, _knockbackForce, 0.2f); // duração de 0.2s
                GameManager.Instance.DeacreaseHealth();
            }
        }

        if (collision.CompareTag("playerAttack"))
        {
            Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.soldieHurt);
            ApplyKnockback(knockbackDir, _knockbackForce, _knockbackDuration);
            StartCoroutine(FlashDamage());
            _hp -= 1;

            if (_hp <= 0)
            {
                _isAlive = false;
                _animator.SetTrigger("die");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.soldieDie);
            }
        }
    }

    [System.Obsolete]
    public void ApplyKnockback(Vector2 direction, float force, float duration)
    {
        if (!_isKnocked)
            StartCoroutine(KnockbackRoutine(direction, force, duration));
    }

    [System.Obsolete]
    private System.Collections.IEnumerator KnockbackRoutine(Vector2 direction, float force, float duration)
    {
        _isKnocked = true;
        _rb.velocity = Vector2.zero; // Reset initial velocity
        _rb.AddForce(direction * force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(duration);

        _rb.velocity = Vector2.zero; // Stop movement
        _isKnocked = false;
    }

    private System.Collections.IEnumerator FlashDamage()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
    }


    public void EnableHitbox()
    {
        hitbox.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.GetComponent<BoxCollider2D>().enabled = false;
    }

}
