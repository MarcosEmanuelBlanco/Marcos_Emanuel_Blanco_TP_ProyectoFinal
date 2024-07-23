using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaAventurero : MonoBehaviour
{
    [SerializeField] int puntos;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            EstadoJugador jugador = other.GetComponent<EstadoJugador>();
            jugador.ModificarVidaJugador(-puntos);
            Debug.Log(" PUNTOS DE DAÑO REALIZADOS AL JUGADOR " + puntos);
        }

        if (other.CompareTag("EnemigoBasico"))
        {
            EnemigoPrevisional enemigo = other.GetComponent<EnemigoPrevisional>();
            enemigo.ModificarVidaEnemigoNoJugador(-puntos);
            Debug.Log(" PUNTOS DE DAÑO REALIZADOS AL JUGADOR " + puntos);
        }

        if (other.CompareTag("Invocacion"))
        {
            Invocacion invocacion = other.GetComponent<Invocacion>();
            invocacion.ModificarVidaEnemigo(-puntos);
            Debug.Log(" PUNTOS DE DAÑO REALIZADOS AL JUGADOR " + puntos);
        }
    }
}
