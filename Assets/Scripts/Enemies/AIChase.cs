using System.Collections;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public EnemyBase enemy;
    public EnemyAttackHitbox hitbox;

    private float _distance;
    private Animator _animator;
    private bool _hasAttacked = false;
    private bool _canWalk = true;
    private bool _isWalking = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.isPlayerDead) return;

        _distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        _animator.SetFloat("SpeedX", direction.x);
        _animator.SetBool("isPlayerNear", _isWalking);

        if (direction.x > 0.01f)
            transform.localScale = new Vector3(-1, 1, 1); // olhando pra direita
        else if (direction.x < -0.01f)
            transform.localScale = new Vector3(1, 1, 1); // olhando pra esquerda

        if (_distance < 5 && _distance >= 1.5f && _canWalk)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, enemy.speed * Time.deltaTime);
            _isWalking = true;
        }

        if (_distance < 1.5f && hitbox.canDamage && !_hasAttacked) 
        {
            _animator.SetTrigger("attack");
            AudioManager.Instance.PlaySFX(AudioManager.Instance.soldieAttack);
            _canWalk = false;
            _hasAttacked = true;
            _isWalking = false;
            StartCoroutine(TimeToWalkAfterAttack());
        }

        if (_distance > 5)
        {
            _isWalking = false;
        }

        if (!hitbox.canDamage)
        {
            _hasAttacked = false;
        }

        IEnumerator TimeToWalkAfterAttack()
        {
            yield return new WaitForSeconds(1f);
            _canWalk = true;
        }
    }
}
