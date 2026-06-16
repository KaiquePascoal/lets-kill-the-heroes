using UnityEngine;
using UnityEngine.SceneManagement; // Adicionado para gerenciar as cenas

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private GameObject telaMortePainel; // Arraste seu Painel de UI aqui no Inspector

    public bool isPlayerDead = false;

    private void Start()
    {
        // Garante que o jogo comece no tempo normal e com a tela de morte escondida
        Time.timeScale = 1f;
        if (telaMortePainel != null)
            telaMortePainel.SetActive(false);
    }

    private void Update()
    {
        PlayerIsDead();
    }

    public void IncreaseHealth()
    {
        if (_player.transform.localScale.x <= 1.3)
            _player.transform.localScale = new Vector2(_player.transform.localScale.x + .1f, _player.transform.localScale.y + .1f);
    }

    public void DeacreaseHealth()
    {
        if (_player.transform.localScale.x >= .7)
            _player.transform.localScale = new Vector2(_player.transform.localScale.x - .1f, _player.transform.localScale.y - .1f);
    }

    public void InstaDeath()
    {
        _player.isDead = true;
        _player.playerAnimator.SetTrigger("isDead");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.die);
        
        // Ativa o painel de Game Over quando o jogador morre
        if (telaMortePainel != null)
        {
            telaMortePainel.SetActive(true);
        }
    }

    private void PlayerIsDead()
    {
        isPlayerDead = _player.isDead;
    }

    // NOVA FUNÇÃO: Vincule esta função ao evento On Click() do seu botão de UI
    public void ReiniciarFase()
    {
        // Pega o nome da cena atual (seja Fase 1, Fase 2, etc.) e recarrega
        string cenaAtual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(cenaAtual);
    }
}
