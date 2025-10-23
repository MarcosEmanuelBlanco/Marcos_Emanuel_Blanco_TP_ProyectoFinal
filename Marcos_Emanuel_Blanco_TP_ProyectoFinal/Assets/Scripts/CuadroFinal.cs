using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CuadroFinal : MonoBehaviour
{
    [SerializeField] private GameObject manager;
    private void OrdenPararTiempo()
    {
        manager.GetComponent<GameManager>().PararTiempo();
    }

    public void BotonContinuar()
    {
        manager.GetComponent<GameManager>().RevivirJugador();
        gameObject.SetActive(false);
    }

    public void BotonReiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BotonVolver()
    {
        SceneManager.LoadScene(2);
    }

    public void BotonSiguienteNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
