using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenuPrincipal : MonoBehaviour
{
    [SerializeField] private GameObject pantallaNiveles;
    [SerializeField] private GameObject pantallaControles;
    [SerializeField] private GameObject pantallaCreditos;
    [SerializeField] private GameObject pantallaMenu;
    public void CargarSiguienteEscena()
    {
        //int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(indiceEscenaActual + 1);
        pantallaMenu.SetActive(false);
        pantallaNiveles.SetActive(true);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }

    public void Controles()
    {
        pantallaMenu.SetActive(false);
        pantallaControles.SetActive(true);
        //SceneManager.LoadScene(7);
    }

    public void Creditos()
    {
        pantallaMenu.SetActive(false);
        pantallaCreditos.SetActive(true);
        //SceneManager.LoadScene(8);
    }

}
