using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aqui você pode adicionar efeitos sonoros, partículas ou aumentar pontuação
            _particleSystem.Play();
            _spriteRenderer.enabled = false;
            _boxCollider.enabled = false;
            GameManager.Instance.IncreaseHealth();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.collectible);
        }
    }
}
