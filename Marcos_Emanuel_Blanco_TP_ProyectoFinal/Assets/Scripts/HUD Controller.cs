using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoVidaJugador;
    [SerializeField] TextMeshProUGUI textoAlmasJugador;
    [SerializeField] TextMeshProUGUI textoPuntosJugador;
    [SerializeField] TextMeshProUGUI textoMunW;
    [SerializeField] TextMeshProUGUI textoMunE;
    [SerializeField] TextMeshProUGUI textoMunR;
    [SerializeField] TextMeshProUGUI textoAleatoria;
    [SerializeField] TextMeshProUGUI textoJuegoDetenido;
    public void ActualizarTextoHUDVida(string nuevoTexto)
    {
        textoVidaJugador.text = "Vida: " + nuevoTexto;
    }

    public void ActualizarTextoHUDAlmas(string nuevoTexto)
    {
        textoAlmasJugador.text = "Almas: " + nuevoTexto;
    }

    public void ActualizarTextoHUDPuntos(string nuevoTexto)
    {
        textoPuntosJugador.text = "Puntos: " + nuevoTexto;
    }

    public void ActualizarTextoHUDMunW(string nuevoTexto)
    {
        textoMunW.text = "W: " + nuevoTexto;
    }

    public void ActualizarTextoHUDMunE(string nuevoTexto)
    {
        textoMunE.text = "E: " + nuevoTexto;
    }

    public void ActualizarTextoHUDMunR(string nuevoTexto)
    {
        textoMunR.text = "R: " + nuevoTexto;
    }

    public void ActualizarTextoHUDAleatoria(string nuevoTexto)
    {
        textoAleatoria.text = "Habilidad Aleatoria: " + nuevoTexto;
    }

    public void ActualizarTextoHUDJuegoDetenido(string nuevoTexto)
    {
        textoJuegoDetenido.text = nuevoTexto;
    }
}
