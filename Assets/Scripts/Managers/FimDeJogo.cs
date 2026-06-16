using UnityEngine;

public class FimDeJogo : MonoBehaviour
{
    // Arraste o Panel do Canvas para cá no Inspector
    public GameObject telaAgradecimento;

    // Executa quando o jogador encosta no final da fase
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ExibirTelaFinal();
        }
    }

    void ExibirTelaFinal()
    {
        telaAgradecimento.SetActive(true); // Mostra a tela
        Time.timeScale = 0f; // Pausa o jogo de fundo
    }

    // Função que o botão vai chamar
    public void FecharJogo()
    {
        Debug.Log("Fechando o jogo...");
        Application.Quit(); // Fecha o jogo (só funciona no jogo buildado)
    }
}
