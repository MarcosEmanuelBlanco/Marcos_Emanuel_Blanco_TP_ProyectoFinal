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
        Ataque();
    }
    private bool DetectarAtacando()
    {
        return gameObject.GetComponent<MovimientoEnemigo>().GetAtacando();
    }

    private bool DetectarAturdido()
    {
        return gameObject.GetComponent<MovimientoEnemigo>().GetAturdido();
    }

    // Update is called once per frame
    void Update()
    {
        DetectarAtacando();
        DetectarAturdido();
    }

    private void FixedUpdate()
    {

    }

    void Disparo()
    {
        if (/*Mathf.Abs(transform.position.x - jugador.transform.position.x) <= gameObject.GetComponent<MovimientoEnemigo>().GetDistancia() && */
            DetectarAtacando() == true && DetectarAturdido() == false)
        {
            GameObject nuevoProyectil = proyectil;
            nuevoProyectil.transform.position = puntoDisparo.transform.position;
            Instantiate(nuevoProyectil);
        }
    }

    void Ataque()
    {
        InvokeRepeating(nameof(Disparo), 0, frecuenciaDisparo);
    }

    private void OnDrawGizmos()
    {

    }
}
