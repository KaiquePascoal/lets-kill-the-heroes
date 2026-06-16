using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{
    public Transform[] pontos; // Arraste os pontos de destino aqui
    public float velocidade = 3f;
    private int indiceAtual = 0;

    void Update()
    {
        if (pontos.Length == 0) return;

        // Move a plataforma em direção ao ponto atual
        transform.position = Vector3.MoveTowards(transform.position, pontos[indiceAtual].position, velocidade * Time.deltaTime);

        // Se chegou muito perto do ponto, muda para o próximo
        if (Vector3.Distance(transform.position, pontos[indiceAtual].position) < 0.1f)
        {
            indiceAtual = (indiceAtual + 1) % pontos.Length;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }
}
