using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int life = 3;
    private bool estaMorto = false;

    private void Start()
    {
        Debug.Log("Vida Inicial: " + life);
    }

    public void TakeDamage(int damage)
    {
        if (estaMorto) return;

        life -= damage;
        Debug.Log("Vida Atual: " + life);

        // 1. Sempre que toma dano da maçã, diminui a escala do jogador (efeito visual do seu GameManager)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.DeacreaseHealth();
        }

        // 2. Checa se a vida zerou para ativar a morte
        if (life <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        estaMorto = true;
        Debug.Log("O Personagem Morreu!");

        // 3. Chama a função do seu GameManager que ativa a animação e o som de morte
        if (GameManager.Instance != null)
        {
            GameManager.Instance.InstaDeath();
        }
    }
}
