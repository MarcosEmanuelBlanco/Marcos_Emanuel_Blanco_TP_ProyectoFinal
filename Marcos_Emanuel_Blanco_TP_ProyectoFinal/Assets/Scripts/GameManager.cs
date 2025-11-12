using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool juegoDetenido;
    [SerializeField] private GameObject dhaork;
    [SerializeField] private int enemigosDerribados;
    [SerializeField] private int totalEnemigos;
    [SerializeField] private GameObject HUDPrincipal;
    [SerializeField] private GameObject cartelDerrotaConAlmas;
    [SerializeField] private GameObject cartelDerrotaSinAlmas;
    [SerializeField] private GameObject cartelVictoria;
    [SerializeField] private GameObject cartelPausa;
    [SerializeField] private UnityEvent<string> OnRemainingSoulsChange;
    private AudioSource sonidosJuego;
    [SerializeField] private AudioClip temaJefe;
    void Start()
    {
        sonidosJuego = GetComponent<AudioSource>();
        enemigosDerribados = 0;
        juegoDetenido = false;
        cartelPausa.SetActive(false);
    }

    public void ReproducirTemaJefe()
    {
        sonidosJuego.clip = temaJefe;
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
        sonidosJuego.Play();
        dhaork.GetComponent<EstadoJugador>().Revivir();
        dhaork.GetComponent<EstadoJugador>().MuertoFalse();
        HUDPrincipal.SetActive(true);
        //RestaurarTiempo();
    }
    void Update()
    {
        Derribado();
        RevivirOReiniciar();
        AreaAsegurada();
        GrulgoshDerrotado();
        Pausa();
        VolverAlMainPausa();
    }

    public void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (juegoDetenido)
            {
                ContinuarJuego();
            }
            else
            {
                cartelPausa.SetActive(true);
                juegoDetenido = true;
                PararTiempo();
            }
        }
    }

    private void VolverAlMainPausa()
    {
        if (juegoDetenido)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                juegoDetenido = false;
                SceneManager.LoadScene(0);
            }
        }
    }

    public void ContinuarJuego()
    {
        cartelPausa.SetActive(false);
        juegoDetenido = false;
        RestaurarTiempo();
    }

    void Derribado()
    {
        if (dhaork.GetComponent<EstadoJugador>().GetFinalMuerte())
        {
            HUDPrincipal.SetActive(false);
            juegoDetenido = true;
        }
        else
        {
            HUDPrincipal.SetActive(true);
            juegoDetenido = false;
        }
    }

    void PararPorEvento()
    {
        if (!juegoDetenido)
        {
            RestaurarTiempo();
        }
        else
        {
            PararTiempo();
        }
    }

    //void ReiniciarEscena()
    //{
    //    Time.timeScale = 1;
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    juegoDetenido = false;
        
    //}

    //void VolverAlMenu()
    //{
    //    Time.timeScale = 1;
    //    SceneManager.LoadScene(2);
    //    juegoDetenido = false;
    //}

    void RevivirOReiniciar()
    {
        if (dhaork.GetComponent<EstadoJugador>().GetAlmas() > 0 && dhaork.GetComponent<EstadoJugador>().GetVidaActual() <= 0)
        {
            sonidosJuego.Pause();
            cartelDerrotaConAlmas.SetActive(true);
            //PararTiempo();
            dhaork.GetComponent<Movimiento>().CambiarAturdido(true);
            dhaork.GetComponent<AtaquesPrevisional>().enabled = false;
        }
        else if(dhaork.GetComponent<EstadoJugador>().GetAlmas() <= 0 && dhaork.GetComponent<EstadoJugador>().GetVidaActual() <= 0)
        {
            sonidosJuego.Pause();
            cartelDerrotaSinAlmas.SetActive(true);
            //PararTiempo();
            dhaork.GetComponent<Movimiento>().CambiarAturdido(true);
            dhaork.GetComponent<AtaquesPrevisional>().enabled = false;
        }
    }

    private void AreaAsegurada()
    {
        if (enemigosDerribados == totalEnemigos && SceneManager.GetActiveScene().buildIndex != 5)
        {
            sonidosJuego.Pause();
            cartelVictoria.SetActive(true);
            dhaork.GetComponent<Movimiento>().enabled = false;
            dhaork.GetComponent<AtaquesPrevisional>().enabled = false;
            HUDPrincipal.SetActive(false);
            //PararTiempo();
        }

    }

    private void GrulgoshDerrotado()
    {
        GameObject gulgo = GameObject.FindGameObjectWithTag("Jefe");
        if(gulgo != null && gulgo.GetComponent<MuerteGrulgosh>().GetGMuerto())
        {
            sonidosJuego.Pause();
            cartelVictoria.SetActive(true);
            dhaork.GetComponent<Movimiento>().enabled = false;
            dhaork.GetComponent<AtaquesPrevisional>().enabled = false;
            HUDPrincipal.SetActive(false);
            //PararTiempo();
        }
    }
}
