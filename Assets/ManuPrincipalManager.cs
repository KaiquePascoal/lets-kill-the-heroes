using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string SampleScene;

    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelInstruction;
    [SerializeField] private GameObject painelCredits;
    [SerializeField] private GameObject logo;

    [Header("Instruções")]
    [SerializeField] private GameObject imagemControle;
    [SerializeField] private GameObject imagemTeclado;

    private bool mostrandoTeclado = false;

    public void Jogar()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void AbrirInstruction()
    {
        painelMenuInicial.SetActive(false);
        painelInstruction.SetActive(true);
        logo.SetActive(false);

        // Começa mostrando o controle
        imagemControle.SetActive(true);
        imagemTeclado.SetActive(false);
        mostrandoTeclado = false;
    }

    public void FecharInstruction()
    {
        painelInstruction.SetActive(false);
        painelMenuInicial.SetActive(true);
        logo.SetActive(true);
    }

    // Botão Trocar
    public void TrocarControles()
    {
        mostrandoTeclado = !mostrandoTeclado;

        imagemControle.SetActive(!mostrandoTeclado);
        imagemTeclado.SetActive(mostrandoTeclado);
    }

    public void AbrirCredits()
    {
        painelMenuInicial.SetActive(false);
        painelCredits.SetActive(true);
        logo.SetActive(false);
    }

    public void FecharCredits()
    {
        painelCredits.SetActive(false);
        painelMenuInicial.SetActive(true);
        logo.SetActive(true);
    }

    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}