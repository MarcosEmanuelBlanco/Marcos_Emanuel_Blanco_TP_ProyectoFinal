using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemigoPrevisional : MonoBehaviour
{
    [SerializeField] private Canvas barraVida;
    [SerializeField] private float vidaMaxima;
    [SerializeField] private int valorEnemigo;
    [SerializeField] private UnityEvent<string> OnHealthChange;
    [SerializeField] private TextMeshProUGUI textoVida;
    [SerializeField] private float vidaActual;
    [SerializeField] private AudioClip efectoMuerte;
    private Animator animatorMov;
    private AudioSource sonido;
    private Rigidbody2D rigidbody2;
    private Collider2D collider2;
    private bool muerto;

    private void Start()
    {
        vidaActual = vidaMaxima;
        OnHealthChange.Invoke(vidaActual.ToString());
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemigo"), LayerMask.NameToLayer("Enemigo"), true);
        animatorMov = GetComponent<Animator>();
        sonido = GetComponent<AudioSource>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        collider2 = GetComponent<Collider2D>();
        muerto = false;
        
    }
    public void ModificarVidaEnemigo(float puntos)
    {
        vidaActual += puntos;
        OnHealthChange.Invoke(vidaActual.ToString());
        barraVida.GetComponent<ActualizarTextoVidaEnemigo>().ActivarAnimaciones();
        Muerte();
    }

    public void CurarVidaEnemigo(float puntos)
    {
        vidaActual += puntos;
        OnHealthChange.Invoke(vidaActual.ToString());
        barraVida.GetComponent<ActualizarTextoVidaEnemigo>().ActivarAnimacionesCuracion();
    }

    public bool GetMuerto()
    {
        return muerto;
    }

    private void Muerte()
    {
        if (vidaActual <= 0)
        {
            muerto = true; 
            barraVida.gameObject.SetActive(false);
            collider2.enabled = false;
            rigidbody2.Sleep();
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                GameObject jugador = GameObject.FindGameObjectWithTag("Player");
                if (jugador == null)
                {
                    return;
                }
                jugador.GetComponent<EstadoJugador>().SumarPuntos(valorEnemigo);
            }
            animatorMov.SetBool("Muerto", true);
            
        }
    }

    public void ModificarVidaEnemigoNoJugador(float puntos)
    {
        vidaActual += puntos;
        OnHealthChange.Invoke(vidaActual.ToString());
        barraVida.GetComponent<ActualizarTextoVidaEnemigo>().ActivarAnimaciones();
        MuerteNoJugador();
    }

    private void MuerteNoJugador()
    {
        if (vidaActual <= 0)
        {
            barraVida.gameObject.SetActive(false);
            collider2.enabled = false;
            rigidbody2.Sleep();
            animatorMov.SetBool("Muerto", true);
        }
    }

    private void DestruirEnemigo()
    {
        GameObject controlador = GameObject.FindGameObjectWithTag("GameController");
        controlador.GetComponent<GameManager>().ContarDerribados();
        Destroy(gameObject);
    }

    private void SonidoMuerte()
    {
        sonido.PlayOneShot(efectoMuerte);
    }

    public float GetVidaActual()
    {
        return vidaActual;
    }
}
