using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemigoPrevisional : MonoBehaviour
{
    [SerializeField] private float vidaMaxima;
    [SerializeField] private int valorEnemigo;
    [SerializeField] private UnityEvent<string> OnHealthChange;
    private float vidaActual;

    private void Start()
    {
        vidaActual = vidaMaxima;
        OnHealthChange.Invoke(vidaActual.ToString());
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemigo"), LayerMask.NameToLayer("Enemigo"), true);
    }
    public void ModificarVidaEnemigo(float puntos)
    {
        vidaActual += puntos;
        OnHealthChange.Invoke(vidaActual.ToString());
        Debug.Log("Enemigo herido");
        Muerte();
    }

    private void Muerte()
    {
        if (vidaActual <= 0)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                GameObject jugador = GameObject.FindGameObjectWithTag("Player");
                if (jugador == null)
                {
                    return;
                }
                jugador.GetComponent<EstadoJugador>().SumarPuntos(valorEnemigo);
            }
            Destroy(gameObject);
        }
    }

    public void ModificarVidaEnemigoNoJugador(float puntos)
    {
        vidaActual += puntos;
        OnHealthChange.Invoke(vidaActual.ToString());
        Debug.Log("Enemigo herido");
        MuerteNoJugador();
    }

    private void MuerteNoJugador()
    {
        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float GetVidaActual()
    {
        return vidaActual;
    }
}
