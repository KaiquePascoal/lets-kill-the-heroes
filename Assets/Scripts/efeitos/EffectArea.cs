using UnityEngine;

public class EffectArea : MonoBehaviour
{
    public PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.isOnEffectArea = true;
            player.playerAnimator.SetBool("isOnEffectArea", true);
            Debug.Log("Entrou");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.isOnEffectArea = false;
            player.playerAnimator.SetBool("isOnEffectArea", false);
            Debug.Log("Saiu");
        }
    }
}
