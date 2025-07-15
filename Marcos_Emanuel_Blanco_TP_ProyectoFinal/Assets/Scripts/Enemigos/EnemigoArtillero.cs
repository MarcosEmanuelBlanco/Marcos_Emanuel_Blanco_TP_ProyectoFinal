using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoArtillero : MonoBehaviour
{
    [SerializeField] private GameObject proyectil;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float frecuenciaDisparo;
    [SerializeField] private GameObject setParticulas;
    private Animator animatorMov;
    private PoolProyectilArtilleria poolBombas;
    private bool activarParticulas;
    void Start()
    {
        activarParticulas = true;
        animatorMov = GetComponent<Animator>();
        poolBombas = GetComponent<PoolProyectilArtilleria>();
    }

    public void ActivarAnimacionAtaque()
    {
        animatorMov.SetTrigger("Atacando");
        //animatorMov.SetBool("Persiguiendo", false);
    }

    private void OnBecameVisible()
    {
        Ataque();
    }

    void Ataque()
    {
        //RaycastHit2D rayoSensorBorde = Physics2D.Raycast(transform.position, Vector2.left, 10);
        //if (rayoSensorBorde.transform.CompareTag("Player") == true || rayoSensorBorde.transform.CompareTag("Aventurero") == true)
        //GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating(nameof(ActivarAnimacionAtaque), 0, frecuenciaDisparo);
        
    }
    private bool DetectarAtacando()
    {
        return gameObject.GetComponent<MovimientoEnemigo>().GetAtacando();
    }

    private bool DetectarAturdido()
    {
        return gameObject.GetComponent<MovimientoEnemigo>().GetAturdido();
    }
    void Update()
    {
        DetectarAtacando();
        DetectarAturdido();
        ActivacionParticulas();
    }

    void Disparo()
    {
        if (/*Mathf.Abs(transform.position.x - jugador.transform.position.x) <= gameObject.GetComponent<MovimientoEnemigo>().GetDistancia() && */
            DetectarAtacando() == true && DetectarAturdido() == false)
        {
            GenerarBomba();
            activarParticulas = false;
        }
        else
        {
            activarParticulas = true;
        }
    }

    private void GenerarBomba()
    {
        GameObject pooledBombas = poolBombas.GetPooledBomba();
        if (pooledBombas != null)
        {
            pooledBombas.transform.position = puntoDisparo.position;
            pooledBombas.SetActive(true);
            pooledBombas.GetComponent<ProyectilEnemigo>().FuerzaCanonazo();
            pooledBombas.GetComponent<ProyectilEnemigo>().ActivarRotacion();
        }
        //GameObject nuevoProyectil = proyectil;
        //nuevoProyectil.transform.position = puntoDisparo.transform.position;
        //Instantiate(nuevoProyectil);
    }

    private void ActivacionParticulas()
    {
        if (!activarParticulas)
        {
            setParticulas.SetActive(false);
        }
        else
        {
            setParticulas.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {

    }
}
