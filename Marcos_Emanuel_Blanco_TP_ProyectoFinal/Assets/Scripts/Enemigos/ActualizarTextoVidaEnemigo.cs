using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActualizarTextoVidaEnemigo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoVidaEnemigo;
    [SerializeField] GameObject barra;
    public void ActualizarTextoHUDVida(string nuevoTexto)
    {
        textoVidaEnemigo.text = nuevoTexto;
    }

    public void ActivarAnimaciones()
    {
        barra.GetComponent<Animator>().SetTrigger("Golpe");
        textoVidaEnemigo.GetComponent<Animator>().SetTrigger("Golpe");
    }

    public void ActivarAnimacionesCuracion()
    {
        textoVidaEnemigo.GetComponent<Animator>().SetTrigger("Curacion");
    }
}
