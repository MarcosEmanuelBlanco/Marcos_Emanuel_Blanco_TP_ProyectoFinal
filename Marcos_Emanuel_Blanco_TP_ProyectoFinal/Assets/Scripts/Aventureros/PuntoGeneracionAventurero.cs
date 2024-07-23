using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoGeneracionAventurero : MonoBehaviour
{
    [SerializeField] private GameObject[] aventureros;
    private int rngAventurero;
    [SerializeField] private Transform posicionSensor;
    [SerializeField] private Transform posicionSpawner;
    [SerializeField] private Vector2 alcanceSensor;
    private bool activo;
    // Start is called before the first frame update
    void Start()
    {
        activo = true;
    }

    private void OnBecameVisible()
    {
        
    }

    private void Deteccion()
    {
        //RaycastHit2D rayoSensorBorde = Physics2D.Raycast(posicionSensor.position, Vector2.down, alcanceSensor);
        //if (rayoSensorBorde.transform.CompareTag("Player") == true)
        //{
            
        //}

        Collider2D[] areaGolpe = Physics2D.OverlapBoxAll(posicionSensor.position, alcanceSensor, 0);
        foreach (Collider2D col in areaGolpe)
        {
            if (col.CompareTag("Player") && activo)
            {
                Invoke(nameof(AparecerAventurero), 0);
                activo = false;
            }
        }
    }

    private void AparecerAventurero()
    {
        rngAventurero = Random.Range(0, aventureros.Length);
        Instantiate(aventureros[rngAventurero], posicionSpawner.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(posicionSensor.position, alcanceSensor);
    }
    // Update is called once per frame
    void Update()
    {
        Deteccion();
    }
}
