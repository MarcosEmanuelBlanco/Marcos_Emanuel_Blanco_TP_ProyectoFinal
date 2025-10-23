using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool puedeVolver;
    [SerializeField] private bool juegoDetenido;
    private bool gulgoVivo;
    [SerializeField] private GameObject dhaork;
    [SerializeField] private int enemigosDerribados;
    [SerializeField] private int totalEnemigos;
    [SerializeField] private GameObject HUDPrincipal;
    [SerializeField] private GameObject cartelDerrotaConAlmas;
    [SerializeField] private GameObject cartelDerrotaSinAlmas;
    [SerializeField] private GameObject cartelVictoria;
    [SerializeField] private GameObject cartelPausa;
    [SerializeField] private UnityEvent<string> OnRemainingSoulsChange;
    void Start()
    {
        enemigosDerribados = 0;
        juegoDetenido = false;
        puedeVolver = true;
        gulgoVivo = true;
        cartelPausa.SetActive(false);
    }

    public void ContarDerribados()
    {
        enemigosDerribados++;
    }
    
    public void PararTiempo()
    {
        Time.timeScale = 0;
    }

    public void RestaurarTiempo()
    {
        Time.timeScale = 1;
    }

    public void RevivirJugador()
    {
        dhaork.GetComponent<EstadoJugador>().Revivir();
        dhaork.GetComponent<EstadoJugador>().MuertoFalse();
        HUDPrincipal.SetActive(true);
    }
    void Update()
    {
        Derribado();
        RevivirOReiniciar();
        AreaAsegurada();
        GrulgoshDerrotado();
        Pausa();
    }

    public void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cartelPausa.SetActive(true);
            juegoDetenido = true;
        }
        //ContinuarJuego();
    }

    public void ContinuarJuego()
    {
        if (Input.GetKeyDown(KeyCode.Space) && juegoDetenido)
        {
            cartelPausa.SetActive(false);
            juegoDetenido = false;
        }
    }

    void Derribado()
    {
        if (dhaork.GetComponent<EstadoJugador>().GetFinalMuerte())
        {
            HUDPrincipal.SetActive(false);
            Time.timeScale = 0;
            juegoDetenido = true;
        }
        else
        {
            HUDPrincipal.SetActive(true);
            juegoDetenido = false;
            Time.timeScale = 1;

        }
    }

    void ReiniciarEscena()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        juegoDetenido = false;
        
    }

    void VolverAlMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
        juegoDetenido = false;
    }

    void RevivirOReiniciar()
    {
        if (dhaork.GetComponent<EstadoJugador>().GetAlmas() > 0 && dhaork.GetComponent<EstadoJugador>().GetVidaActual() <= 0)
        {
            cartelDerrotaConAlmas.SetActive(true);
            dhaork.GetComponent<Movimiento>().CambiarAturdido(true);
            dhaork.GetComponent<AtaquesPrevisional>().enabled = false;
            puedeVolver = true;
        }
        else if(dhaork.GetComponent<EstadoJugador>().GetAlmas() <= 0 && dhaork.GetComponent<EstadoJugador>().GetVidaActual() <= 0)
        {
            cartelDerrotaSinAlmas.SetActive(true);
            dhaork.GetComponent<Movimiento>().CambiarAturdido(true);
            dhaork.GetComponent<AtaquesPrevisional>().enabled = false;
            puedeVolver = false;
        }
    }

    private void AreaAsegurada()
    {
        if (enemigosDerribados == totalEnemigos && SceneManager.GetActiveScene().buildIndex != 5)
        {
            cartelVictoria.SetActive(true);
            dhaork.GetComponent<Movimiento>().enabled = false;
            dhaork.GetComponent<AtaquesPrevisional>().enabled = false;
            HUDPrincipal.SetActive(false);
            juegoDetenido = true;
        }

    }

    private void GrulgoshDerrotado()
    {
        GameObject gulgo = GameObject.FindGameObjectWithTag("Jefe");
        if(gulgo != null && gulgo.GetComponent<MuerteGrulgosh>().GetGMuerto())
        {
            gulgoVivo = false;
            cartelVictoria.SetActive(true);
            dhaork.GetComponent<Movimiento>().enabled = false;
            dhaork.GetComponent<AtaquesPrevisional>().enabled = false;
            HUDPrincipal.SetActive(false);
            juegoDetenido = true;
        }
    }
}
