using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarSiguienteCuadro : MonoBehaviour
{
    [SerializeField] private GameObject teclas;
    [SerializeField] private GameObject siguienteCuadro;

    private void ActivarTeclas()
    {
        teclas.SetActive(true);
    }
    private void Activar()
    {
        gameObject.SetActive(false);
        siguienteCuadro.SetActive(true);
    }
}
