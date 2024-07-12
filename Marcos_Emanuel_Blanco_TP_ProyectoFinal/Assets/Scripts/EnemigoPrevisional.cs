using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPrevisional : MonoBehaviour
{
    [SerializeField] private float vidaEnemigo;
    [SerializeField] private float velocidadEnemigo;
    [SerializeField] private float distanciaAlJugador;
    [SerializeField] private Transform puntoVision;
    //[SerializeField] private Transform representacionAtaque;
    //[SerializeField] private Transform posicionAtaque;
    //[SerializeField] private float radioGolpe;
    //[SerializeField] private float dagnoGolpe;
    public bool atacando;
    [SerializeField] private bool atacadoPorAventurero;
    public int rotacion;
    [SerializeField] private Vector2 alcanceVision;
    public void ModificarVidaEnemigo(float puntos)
    {
        vidaEnemigo += puntos;
        Debug.Log("Enemigo herido");
        Muerte();
    }

    private void Muerte()
    {
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        atacando = false;
        //representacionAtaque.gameObject.SetActive(false);
        atacadoPorAventurero = false;
        
    }

    private void OnBecameVisible()
    {
        
        //Ataque();
    }

    void Update()
    {
        Movimiento();
        
        CambiarMoviendose();
    }

    private void FixedUpdate()
    {
        
    }

    void Movimiento()
    {
        if (atacadoPorAventurero)
        {
            Rotar();
        }
        Collider2D[] areaVision = Physics2D.OverlapBoxAll(puntoVision.position, alcanceVision, 0);
        foreach (Collider2D col in areaVision)
        {
            if (!atacadoPorAventurero && col.CompareTag("Player"))
            {
                MovimientoHaciaJugador();
            }
            if(atacadoPorAventurero && col.CompareTag("Aventurero"))
            {
                MovimientoHaciaAventurero();
            }
        }
    }
    void MovimientoHaciaAventurero()
    {
        
        GameObject aventurero = GameObject.FindGameObjectWithTag("Aventurero");
        
        if (aventurero != null && Mathf.Abs(transform.position.x - aventurero.transform.position.x) > distanciaAlJugador && transform.position.x < aventurero.transform.position.x)
        {
            transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
        }else if (aventurero != null && Mathf.Abs(transform.position.x - aventurero.transform.position.x) < distanciaAlJugador && transform.position.x > aventurero.transform.position.x)
        {
            transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
        }
        else { atacando = true; }
    }
    void Rotar()
    {
        transform.SetLocalPositionAndRotation(transform.position, Quaternion.Euler(0, rotacion, 0));       
    }
    void MovimientoHaciaJugador()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (Mathf.Abs(transform.position.x - jugador.transform.position.x) > distanciaAlJugador)
        {           
            transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.left);
        }
        else if (Mathf.Abs(transform.position.x - jugador.transform.position.x) < distanciaAlJugador)
        {
            atacando = true;
        }
        
    }
    public bool GetMoviendose()
    {
        return atacando;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(puntoVision.position, alcanceVision);
    }
    void CambiarMoviendose()
    {
        if (atacadoPorAventurero)
        {
            atacando = true;
        }
        else { atacando = false; }
    }
    public bool GetAtacado()
    {
        return atacadoPorAventurero;
    }

    public void CambiarAtacado(bool atq)
    {
        atacadoPorAventurero = atq;
    }
}
