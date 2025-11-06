using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueCACEnemigo : MonoBehaviour
{
    //[SerializeField] private Transform representacionAtaque;
    [SerializeField] private Transform posicionAtaque;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dagnoGolpe;
    [SerializeField] private float frecuenciaAtaque;
    [SerializeField] private GameObject efectoAtaque;
    [SerializeField] private AudioClip efectoMusgSonido;
    private Animator animatorMov;
    private AudioSource sonidoMusgardo;
    // Start is called before the first frame update
    void Start()
    {
        //representacionAtaque.gameObject.SetActive(false);
        animatorMov = GetComponent<Animator>();
        sonidoMusgardo = GetComponent<AudioSource>();
    }
    private void OnBecameVisible()
    {
        Ataque();
    }

    private void ActivarAnimacionAtaque()
    {
        //animatorMov.SetBool("Persiguiendo", false);
        animatorMov.SetTrigger("Atacando");
    }

    private void SonidoMusgAtaque()
    {
        sonidoMusgardo.PlayOneShot(efectoMusgSonido);
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

    void Ataque()
    {
        InvokeRepeating(nameof(ActivarAnimacionAtaque), 0, frecuenciaAtaque);
    }

    private void ActivarEfecto()
    {
        efectoAtaque.SetActive(true);
    }

    void Golpear()
    {
        if (DetectarAtacando() == true && DetectarAturdido() == false)
        {
            Collider2D[] areaGolpe = Physics2D.OverlapCircleAll(posicionAtaque.position, radioGolpe);
            foreach (Collider2D col in areaGolpe)
            {
                if (col.CompareTag("Player"))
                {
                    //ActivarAtaque();
                    col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoGolpe);
                    Debug.Log("Jugador Herido");
                }else if (col.CompareTag("Aventurero"))
                {
                    //ActivarAtaque();
                    col.transform.GetComponent<Aventurero>().ModificarVidaEnemigoNoJugador(-dagnoGolpe);
                    Debug.Log("Enemigo Herido");
                }else if (col.CompareTag("Invocacion"))
                {
                    //ActivarAtaque();
                    col.transform.GetComponent<Invocacion>().ModificarVidaInvocacion(-dagnoGolpe);
                }
            }
        }
    }

    //private void ActivarAtaque()
    //{
    //    representacionAtaque.gameObject.SetActive(true);
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(posicionAtaque.position, radioGolpe);
    }
}
