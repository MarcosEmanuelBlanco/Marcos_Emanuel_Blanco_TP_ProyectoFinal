using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteGrulgosh : MonoBehaviour
{
    private bool gMuerto;

    private void Start()
    {
        gMuerto = false;
    }
    public void ActivarDestierro()
    {
        if (gameObject.GetComponent<EnemigoPrevisional>().GetMuerto())
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");
            jugador.GetComponent<EstadoJugador>().Destierro();
        }
    }

    private void SetGMuerto()
    {
        gMuerto = true;
    }

    public bool GetGMuerto()
    {
        return gMuerto;
    }
}
