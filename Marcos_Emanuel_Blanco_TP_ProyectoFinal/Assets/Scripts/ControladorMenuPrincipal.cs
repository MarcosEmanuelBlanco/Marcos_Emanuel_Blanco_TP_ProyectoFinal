using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenuPrincipal : MonoBehaviour
{
    public void CargarSiguienteEscena()
    {
        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceEscenaActual + 1);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }

    public void Controles()
    {
        SceneManager.LoadScene(7);
    }

    public void Creditos()
    {
        SceneManager.LoadScene(8);
    }

}
