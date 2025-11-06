using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAlMain : MonoBehaviour
{
    [SerializeField] private GameObject pantallaControles;
    [SerializeField] private GameObject pantallaCreditos;
    [SerializeField] private GameObject pantallaNiveles;
    [SerializeField] private GameObject pantallaObjetivo;
    public void VolverAlMainMenu()
    {
        pantallaControles.SetActive(false);
        pantallaObjetivo.SetActive(true);
        //SceneManager.LoadScene(0);
    }

    public void VolverAlMainMenuCreditos()
    {
        pantallaCreditos.SetActive(false);
        pantallaObjetivo.SetActive(true);
        //SceneManager.LoadScene(0);
    }

    public void VolverAlMainMenuNiveles()
    {
        pantallaNiveles.SetActive(false);
        pantallaObjetivo.SetActive(true);
        //SceneManager.LoadScene(0);
    }

    public void VolverAlMainMenuPlaceHolder()
    {
        SceneManager.LoadScene(0);
    }
}
