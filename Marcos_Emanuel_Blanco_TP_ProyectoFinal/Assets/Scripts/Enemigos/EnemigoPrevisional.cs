using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemigoPrevisional : MonoBehaviour
{
    [SerializeField] private Canvas barraVida;
    [SerializeField] private float vidaMaxima;
    [SerializeField] private int valorEnemigo;
    [SerializeField] private UnityEvent<string> OnHealthChange;
    private float vidaActual;
    private Animator animatorMov;
    private Rigidbody2D rigidbody2;
    private Collider2D collider2;

    private void Start()
    {
        vidaActual = vidaMaxima;
        OnHealthChange.Invoke(vidaActual.ToString());
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemigo"), LayerMask.NameToLayer("Enemigo"), true);
        animatorMov = GetComponent<Animator>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        collider2 = GetComponent<Collider2D>();
    }
    public void ModificarVidaEnemigo(float puntos)
    {
        vidaActual += puntos;
        OnHealthChange.Invoke(vidaActual.ToString());
        Debug.Log("Enemigo herido");
        Muerte();
    }

    private void Muerte()
    {
        if (vidaActual <= 0)
        {
            barraVida.gameObject.SetActive(false);
            GameObject controlador = GameObject.FindGameObjectWithTag("GameController");
            controlador.GetComponent<GameManager>().contarDerribados();
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
        Debug.Log("Enemigo herido");
        MuerteNoJugador();
    }

    private void MuerteNoJugador()
    {
        if (vidaActual <= 0)
        {
            barraVida.gameObject.SetActive(false);
            GameObject controlador = GameObject.FindGameObjectWithTag("GameController");
            controlador.GetComponent<GameManager>().contarDerribados();
            collider2.enabled = false;
            rigidbody2.Sleep();
            animatorMov.SetBool("Muerto", true);
        }
    }

    private void DestruirEnemigo()
    {
        Destroy(gameObject);
    }

    public float GetVidaActual()
    {
        return vidaActual;
    }
}
