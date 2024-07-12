using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoActivacionOleadas : MonoBehaviour
{
    [SerializeField] private GameObject[] enemigosAGenerar;
    [SerializeField] private Transform[] puntosAparicion;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnBecameVisible()
    {
        Invoke(nameof(AparecerEnemigos),0);
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
        
    }
}
