using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorParaPrimerCuadro : MonoBehaviour
{
    [SerializeField] private GameObject cuadroAGenerar;
    [SerializeField] private Transform posicionSensor;
    [SerializeField] private Vector2 alcanceSensor;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(posicionSensor.position, alcanceSensor);
    }
    private void Deteccion()
    {
        Collider2D[] areaGolpe = Physics2D.OverlapBoxAll(posicionSensor.position, alcanceSensor, 0);
        foreach (Collider2D col in areaGolpe)
        {
            if (col.CompareTag("Player"))
            {
                Invoke(nameof(ActivarSigCuadro), 0);
                gameObject.SetActive(false);
            }
        }
    }

    void ActivarSigCuadro()
    {
        cuadroAGenerar.SetActive(true);
    }

    void Update()
    {
        Deteccion();
    }
}
