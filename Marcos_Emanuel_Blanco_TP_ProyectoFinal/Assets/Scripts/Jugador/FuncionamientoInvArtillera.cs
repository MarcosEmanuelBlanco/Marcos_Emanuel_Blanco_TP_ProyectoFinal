using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionamientoInvArtillera : MonoBehaviour
{
    [SerializeField] private GameObject proyectil;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float frecuenciaDisparo;
    [SerializeField] private float velocidadInvocacion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnBecameVisible()
    {
        Ataque();
    }

    private void FixedUpdate()
    {
        MovimientoInvocacion();
    }

    void MovimientoInvocacion()
    {
        Vector3 newPosition = transform.position + new Vector3(0.1f * velocidadInvocacion, 0f, 0f);
        transform.position = newPosition;
    }

    void Disparo()
    {
        GameObject nuevoProyectil = proyectil;
        nuevoProyectil.transform.position = puntoDisparo.transform.position;
        Instantiate(nuevoProyectil);
    }

    void Ataque()
    {
        //RaycastHit2D rayoSensorBorde = Physics2D.Raycast(transform.position, Vector2.left, 10);
        //if (rayoSensorBorde.transform.CompareTag("Player") == true || rayoSensorBorde.transform.CompareTag("Aventurero") == true)
        //{
            InvokeRepeating(nameof(Disparo), 0, frecuenciaDisparo);
        //}
    }
}
