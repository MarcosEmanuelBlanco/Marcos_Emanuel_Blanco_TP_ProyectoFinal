using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueAventureroTirador : MonoBehaviour
{
    [SerializeField] private GameObject bala;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float tiempoEntreDisparos;
    [SerializeField] private Transform posicionAtaque;
    [SerializeField] private Vector2 areaAtaque;
    private Animator animatorMov;
    private PoolBalas poolBalas;
    void Start()
    {
        animatorMov = GetComponent<Animator>();
        poolBalas = GetComponent<PoolBalas>();
    }

    public void ActivarAnimacionAtaque()
    {
        StartCoroutine(nameof(EsperaAtaque));
    }

    private IEnumerator EsperaAtaque()
    {
        yield return new WaitForSeconds(3);
        animatorMov.SetTrigger("Atacando");
    }

    private void OnBecameVisible()
    {
        Ataque();
    }

    private bool DetectarMoviendose()
    {
        return gameObject.GetComponent<Aventurero>().GetMoviendose();
    }

    private bool DetectarAturdido()
    {
        return gameObject.GetComponent<Aventurero>().GetAturdido();
    }

    private bool DetectarALaDerecha()
    {
        return gameObject.GetComponent<Aventurero>().HaciaDonde();
    }

    private bool DetectarInvulnerable()
    {
        return gameObject.GetComponent<Aventurero>().GetInvulnerable();
    }

    //private void ActivarCambioDirección()
    //{
    //    if (DetectarALaDerecha())
    //    {
    //        bala.CambiarDireccionBala();
    //    }
    //}
    void Update()
    {
        DetectarMoviendose();
        DetectarAturdido();
        DetectarALaDerecha();
        DetectarInvulnerable();
    }

    void Ataque()
    {
        InvokeRepeating(nameof(ActivarAnimacionAtaque), 0, tiempoEntreDisparos);
    }

    private void Disparar()
    {
        if (DetectarMoviendose() == true && DetectarAturdido() == false && DetectarInvulnerable() == false)
        {
            Collider2D[] areaAlcanceArma = Physics2D.OverlapBoxAll(posicionAtaque.position, areaAtaque, 0);
            foreach (Collider2D col in areaAlcanceArma)
            {
                if (col.CompareTag("Player") || col.CompareTag("Invocacion") || col.CompareTag("EnemigoBasico"))
                {
                    GenerarBala();
                }
            }
        }
    }

    private void GenerarBala()
    {
        GameObject pooledBalas = poolBalas.GetPooledBalas();
        if(pooledBalas != null)
        {
            pooledBalas.transform.position = puntoDisparo.position;
            pooledBalas.SetActive(true);
            pooledBalas.GetComponent<BalaAventurero>().FuerzaDisparo();
        }
        //GameObject nuevoProyectil = bala;
        //nuevoProyectil.transform.position = puntoDisparo.transform.position;
        //Instantiate(nuevoProyectil);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(posicionAtaque.position, areaAtaque);
    }
}
