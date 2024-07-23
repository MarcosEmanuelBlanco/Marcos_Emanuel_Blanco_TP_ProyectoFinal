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
    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaJugador;
        puntaje = 0;
        almas = 3;
        OnHealthChange.Invoke(vidaActual.ToString());
        OnSoulsChange.Invoke(almas.ToString());
        OnScoreChange.Invoke(puntaje.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModificarVidaJugador(float puntos)
    {
        vidaActual += puntos;
        OnHealthChange.Invoke(vidaActual.ToString());
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
    }
}
