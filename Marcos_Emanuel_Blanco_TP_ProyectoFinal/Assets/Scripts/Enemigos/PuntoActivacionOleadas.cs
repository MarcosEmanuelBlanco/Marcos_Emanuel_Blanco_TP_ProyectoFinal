using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuntoActivacionOleadas : MonoBehaviour
{
    [SerializeField] private GameObject[] enemigosAGenerar;
    [SerializeField] private Transform[] puntosAparicion;
    [SerializeField] private Transform posicionSensor;
    [SerializeField] private Vector2 alcanceSensor;
    private bool activo;

    // Start is called before the first frame update
    void Start()
    {
        activo = true;
    }

    private void OnBecameInvisible()
    {
        //CancelInvoke(nameof(AparecerEnemigos));
    }

    private void OnBecameVisible()
    {
        //Deteccion();
    }
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
            if (col.CompareTag("Player") && activo)
            {
                Invoke(nameof(AparecerEnemigos), 0);
                activo = false;
            }
        }
    }

    void AparecerEnemigos()
    {
        for (int i = 0; i < enemigosAGenerar.Length; i++)
        {
            Instantiate(enemigosAGenerar[i], puntosAparicion[i].position, Quaternion.identity);

        }
        
    }
    // Update is called once per frame
    void Update()
    {
        Deteccion();
    }
}
