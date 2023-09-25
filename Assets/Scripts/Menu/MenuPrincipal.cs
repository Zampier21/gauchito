using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private string gameNameLevel;
    [SerializeField] private GameObject MenuInicial;
    [SerializeField] private GameObject Opcoes;
    [SerializeField] private GameObject AbrirCreditos;
    public void Play()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void Options()
    {
        MenuInicial.SetActive(false);
        Opcoes.SetActive(true);
        
    }

    public void CloseOptions()
    {
        Opcoes.SetActive(false);
        MenuInicial.SetActive(true);

    }

    public void Creditos()
    {
        MenuInicial.SetActive(false);
        AbrirCreditos.SetActive(true);
    }
    public void CloseCreditos()
    {
        AbrirCreditos.SetActive(false);
        MenuInicial.SetActive(true);
    }
    public void LeaveGame()
    {
        Application.Quit();
        
    }
}
