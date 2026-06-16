using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarCena : MonoBehaviour
{
    // Nome exato da cena da Fase 2 que você quer carregar
    public string nomeProximaFase = "fase2";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu é o jogador
        if (collision.CompareTag("Player"))
        {
            CarregarFase();
        }
    }

    public void CarregarFase()
    {
        SceneManager.LoadScene(nomeProximaFase);
    }
}
