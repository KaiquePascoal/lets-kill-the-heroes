using System.Collections;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    public int damage = 1;
    public float delayBetweenHits = 3f;
    public bool canDamage = true;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canDamage && collision.CompareTag("Player"))
        {
            canDamage = false;
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            StartCoroutine(DamageCooldown());
            GameManager.Instance.DeacreaseHealth();
        }
    }

    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(delayBetweenHits);
        canDamage = true;
    }
}
