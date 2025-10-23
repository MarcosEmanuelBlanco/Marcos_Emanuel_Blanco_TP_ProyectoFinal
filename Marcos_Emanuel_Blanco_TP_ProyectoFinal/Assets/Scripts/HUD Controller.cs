using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoVidaJugador;
    [SerializeField] TextMeshProUGUI textoAlmasJugador;
    [SerializeField] TextMeshProUGUI textoPuntosJugador;
    [SerializeField] TextMeshProUGUI textoPuntosJugadorFinalConVidas;
    [SerializeField] TextMeshProUGUI textoPuntosJugadorFinalSinVidas;
    [SerializeField] TextMeshProUGUI textoPuntosJugadorFinalVictoria;
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
        textoPuntosJugadorFinalConVidas.text = "Puntos: " + nuevoTexto;
        textoPuntosJugadorFinalSinVidas.text = "Puntos: " + nuevoTexto;
        textoPuntosJugadorFinalVictoria.text = "Puntos: " + nuevoTexto;
    }

    public void ActualizarTextoHUDMunW(string nuevoTexto)
    {
        textoMunW.text = nuevoTexto;
    }

    public void ActualizarTextoHUDMunE(string nuevoTexto)
    {
        textoMunE.text = nuevoTexto;
    }

    public void ActualizarTextoHUDMunR(string nuevoTexto)
    {
        textoMunR.text = nuevoTexto;
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
