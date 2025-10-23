using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimarTexto : MonoBehaviour
{
    [SerializeField] private GameObject siguienteTexto;

    private void ActivarSiguienteTexto()
    {
        siguienteTexto.SetActive(true);
        gameObject.SetActive(false);
    }
}
