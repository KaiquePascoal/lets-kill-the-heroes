using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlePausa : MonoBehaviour
{
    public GameObject menuPausaUI;
    private bool jogoPausado = false;

    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (jogoPausado)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Continuar()
    {
        menuPausaUI.SetActive(false); 
        Time.timeScale = 1f;          
        jogoPausado = false;
    }

    void Pausar()
    {
        menuPausaUI.SetActive(true);  
        Time.timeScale = 0f;          
        jogoPausado = true;
    }

    public void ReiniciarFase()
    {
        Time.timeScale = 1f;      

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VoltarMenuPrincipal()
    {
        Time.timeScale = 1f;      
        SceneManager.LoadScene(0);
    }
}