using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoGeneracionAventurero : MonoBehaviour
{
    [SerializeField] private GameObject[] aventureros;
    private int rngAventurero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnBecameVisible()
    {
        rngAventurero = Random.Range(0, aventureros.Length);
        Instantiate(aventureros[rngAventurero], transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
