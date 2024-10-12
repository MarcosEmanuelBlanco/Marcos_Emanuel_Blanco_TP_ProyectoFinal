using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActualizarTextoVidaEnemigo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoVidaEnemigo;
    public void ActualizarTextoHUDVida(string nuevoTexto)
    {
        textoVidaEnemigo.text = nuevoTexto;
    }
}
