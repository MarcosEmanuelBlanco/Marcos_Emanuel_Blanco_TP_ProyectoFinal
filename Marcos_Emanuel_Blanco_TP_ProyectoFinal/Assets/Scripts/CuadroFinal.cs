using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CuadroFinal : MonoBehaviour
{
    [SerializeField] private GameObject manager;

    public void BotonContinuar()
    {
        manager.GetComponent<GameManager>().RevivirJugador();
        gameObject.SetActive(false);
    }

    public void BotonReiniciar()
    {
        //manager.GetComponent<GameManager>().RestaurarTiempo();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BotonVolver()
    {
        //manager.GetComponent<GameManager>().RestaurarTiempo();
        SceneManager.LoadScene(0);
    }

    public void BotonSiguienteNivel()
    {
        //manager.GetComponent<GameManager>().RestaurarTiempo();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
