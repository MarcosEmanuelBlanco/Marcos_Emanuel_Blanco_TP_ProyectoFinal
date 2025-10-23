using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorSeleccion : MonoBehaviour
{
    public void CargarNivel1()
    {
        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceEscenaActual + 1);
    }

    public void CargarNivel2()
    {
        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceEscenaActual + 2);
    }

    public void CargarNivel3()
    {
        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceEscenaActual + 3);
    }

    public void VolverAlMenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
}
