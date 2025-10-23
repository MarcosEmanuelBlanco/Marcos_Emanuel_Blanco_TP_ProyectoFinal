using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarAventurero : MonoBehaviour
{
    [SerializeField] private GameObject generadorAventuero;

    private void ActivarGeneracion()
    {
        generadorAventuero.GetComponent<PuntoGeneracionAventurero>().AparecerAventurero();
    }
}
