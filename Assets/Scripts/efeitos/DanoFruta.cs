using UnityEngine;

public class DanoFruta : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocidadeRotacao = 200f;

    [Header("Efeitos de Impacto")]
    public GameObject particulaImpactoPrefab;
    public AudioClip somImpacto;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Aplica o giro contínuo ao nascer
        float direcaoAleatoria = Random.Range(-1f, 1f) > 0 ? 1f : -1f;
        rb.angularVelocity = direcaoAleatoria * velocidadeRotacao;
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {
        ProcessarColisao(outro.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        ProcessarColisao(colisao.gameObject);
    }

    void ProcessarColisao(GameObject objetoColidido)
    {
        // 1. Se atingiu o Jogador
        if (objetoColidido.CompareTag("Player"))
        {
            // Busca o seu script PlayerHealth no objeto que colidiu
            PlayerHealth playerHealth = objetoColidido.GetComponent<PlayerHealth>();

            // Se encontrou o script, causa 1 de dano
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }

            AtivarEfeitos();
            Destroy(gameObject);
            return;
        }

        // 2. Se atingiu o Chão (Layer groundlayer)
        string nomeDaLayer = LayerMask.LayerToName(objetoColidido.layer).ToLower();
        if (nomeDaLayer == "groundlayer")
        {
            AtivarEfeitos();
            Destroy(gameObject);
        }
    }

    void AtivarEfeitos()
    {
        if (particulaImpactoPrefab != null)
        {
            Instantiate(particulaImpactoPrefab, transform.position, Quaternion.identity);
        }

        if (somImpacto != null)
        {
            // Cria o emissor de som 3D na posição do impacto
            GameObject objetoSom = new GameObject("SomImpactoTemporal");
            objetoSom.transform.position = transform.position;

            AudioSource audioSource = objetoSom.AddComponent<AudioSource>();
            audioSource.clip = somImpacto;
            audioSource.spatialBlend = 1.0f; // Totalmente 3D
            audioSource.minDistance = 2f;
            audioSource.maxDistance = 15f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;

            audioSource.Play();
            Destroy(objetoSom, somImpacto.length);
        }
    }
}
