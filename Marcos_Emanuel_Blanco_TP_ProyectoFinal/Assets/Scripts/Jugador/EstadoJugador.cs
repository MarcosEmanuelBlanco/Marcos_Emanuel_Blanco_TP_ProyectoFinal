using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EstadoJugador : MonoBehaviour
{
    [SerializeField] private float vidaJugador;
    private float vidaActual;
    private int almas;
    private int puntaje;
    [SerializeField] private UnityEvent<string> OnHealthChange;
    [SerializeField] private UnityEvent<string> OnSoulsChange;
    [SerializeField] private UnityEvent<string> OnScoreChange;
    private Animator animatorCuerpo;
    [SerializeField] private GameObject brazo;
    [SerializeField] private GameObject piernas;
    [SerializeField] private bool finalMuerte;
    void Start()
    {
        finalMuerte = false;
        animatorCuerpo = GetComponent<Animator>();
        vidaActual = vidaJugador;
        puntaje = 0;
        almas = 3;
        OnHealthChange.Invoke(vidaActual.ToString());
        OnSoulsChange.Invoke(almas.ToString());
        OnScoreChange.Invoke(puntaje.ToString());
    }

    public void SetFinalMuerte(bool muerto)
    {
        finalMuerte = muerto;
    }

    public void MuertoTrue()
    {
        SetFinalMuerte(true);
    }

    public void MuertoFalse()
    {
        SetFinalMuerte(false);
    }
    public bool GetFinalMuerte()
    {
        return finalMuerte;
    }
    void Update()
    {

    }

    public void ModificarVidaJugador(float puntos)
    {
        vidaActual += puntos;
        OnHealthChange.Invoke(vidaActual.ToString());
        MuerteJugador();
    }

    private void MuerteJugador()
    {
        if (vidaActual <= 0)
        {
            gameObject.GetComponent<Movimiento>().CambiarAturdido(true);
            animatorCuerpo.SetBool("Muerte",true);
            brazo.GetComponent<AnimarBrazo>().Muerte();
            piernas.GetComponent<AnimarPiernas>().Quieto();
        }
    }

    public float GetVidaActual()
    {
        return vidaActual;
    }

    public int GetAlmas()
    {
        return almas;
    }

    public void SumarAlmas(int alma)
    {
        almas += alma;
        OnSoulsChange.Invoke(almas.ToString());
    }

    public void SumarPuntos(int valor)
    {
        puntaje += valor;
        OnScoreChange.Invoke(puntaje.ToString());
    }

    public void Revivir()
    {
        vidaActual = vidaJugador;
        OnHealthChange.Invoke(vidaActual.ToString());
        almas--;
        OnSoulsChange.Invoke(almas.ToString());
        gameObject.GetComponent<AtaquesPrevisional>().enabled = false;
        gameObject.GetComponent<Movimiento>().CambiarAturdido(false);
        animatorCuerpo.SetBool("Muerte", false);
        brazo.GetComponent<AnimarBrazo>().BrazoRevivir();
    }

    public void Destierro()
    {
        animatorCuerpo.SetTrigger("Destierro");
        brazo.GetComponent<AnimarBrazo>().BrazoInerte();
        piernas.GetComponent<AnimarPiernas>().Quieto();
    }
}