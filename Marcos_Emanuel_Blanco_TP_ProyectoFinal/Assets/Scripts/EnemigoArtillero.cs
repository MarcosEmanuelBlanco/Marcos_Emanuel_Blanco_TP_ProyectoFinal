using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoArtillero : MonoBehaviour
{
    [SerializeField] private GameObject proyectil;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float frecuenciaDisparo;
    void Start()
    {

    }

    private void OnBecameInvisible()
    {

    }
    private void OnBecameVisible()
    {
        
    }

    void Update()
    {
        Ataque();
    }

    private void FixedUpdate()
    {

    }

    void Disparo()
    {
        GameObject nuevoProyectil = proyectil;
        nuevoProyectil.transform.position = puntoDisparo.transform.position;
        Instantiate(nuevoProyectil);
    }

    void Ataque()
    {
        RaycastHit2D rayoSensorBorde = Physics2D.Raycast(transform.position, Vector2.left, 10);
        if (rayoSensorBorde.transform.CompareTag("Player") == true || rayoSensorBorde.transform.CompareTag("Aventurero") == true)
        {
            InvokeRepeating(nameof(Disparo), 0, frecuenciaDisparo);
        }
    }

    private void OnDrawGizmos()
    {

    }
}
