using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPrevisional : MonoBehaviour
{
    [SerializeField] private float vidaMaxima;
    [SerializeField] private int valorEnemigo;
    private float vidaActual;

    private void Start()
    {
        vidaActual = vidaMaxima;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemigo"), LayerMask.NameToLayer("Enemigo"), true);
    }
    public void ModificarVidaEnemigo(float puntos)
    {
        vidaActual += puntos;
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
