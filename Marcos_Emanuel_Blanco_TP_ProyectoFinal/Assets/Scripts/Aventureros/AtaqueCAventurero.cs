using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AtaqueCAventurero : MonoBehaviour
{
    [SerializeField] private Transform representacionAtaque;
    [SerializeField] private Transform posicionAtaque;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dagnoGolpe;
    [SerializeField] private float tiempoEntreGolpes;
    private Animator animatorMov;
    void Start()
    {
        representacionAtaque.gameObject.SetActive(false);
        animatorMov = GetComponent<Animator>();
    }

    public void ActivarAnimacionAtaque()
    {
        StartCoroutine(nameof(EsperaAtaque));   
    }

    private IEnumerator EsperaAtaque()
    {
        yield return new WaitForSeconds(4);
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

    private bool DetectarInvulnerable()
    {
        return gameObject.GetComponent<Aventurero>().GetInvulnerable();
    }

    // Update is called once per frame
    void Update()
    {
        DetectarMoviendose();
        DetectarAturdido();
        DetectarInvulnerable();
    }
    void Ataque()
    {
        //Collider2D[] areaGolpe = Physics2D.OverlapCircleAll(posicionAtaque.position, radioGolpe);
        //foreach (Collider2D col in areaGolpe)
        //{
        //    if (col.CompareTag("Player"))
        //    {
        //        InvokeRepeating(nameof(ActivarAnimacionAtaque), 0.5f, tiempoEntreGolpes);
        //    }

        //    if (col.CompareTag("EnemigoBasico"))
        //    {
        //        InvokeRepeating(nameof(ActivarAnimacionAtaque), 0.5f, tiempoEntreGolpes);
        //    }
        //}

        InvokeRepeating(nameof(ActivarAnimacionAtaque), 0.5f, tiempoEntreGolpes);
        
    }

    void Golpear()
    {
        if (DetectarMoviendose() == false && DetectarAturdido() == false && DetectarInvulnerable() == false)
        {
            Collider2D[] areaGolpe = Physics2D.OverlapCircleAll(posicionAtaque.position, radioGolpe);
            foreach (Collider2D col in areaGolpe)
            {
                if (col.CompareTag("Player"))
                {

                    ActivarAtaque();
                    col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoGolpe);
                    Debug.Log("Jugador Herido");
                }

                if (col.CompareTag("EnemigoMina"))
                {
                    ActivarAtaque();
                    col.transform.GetComponent<AtaqueMina>().ModificarVidaEnemigo(-dagnoGolpe);
                    Debug.Log("Enemigo Herido");
                }

                if (col.CompareTag("EnemigoBasico"))
                {
                    ActivarAtaque();
                    col.transform.GetComponent<MovimientoEnemigo>().CambiarAtqAventurero(true);
                    if(col.transform.position.x < transform.position.x)
                    {
                        col.transform.GetComponent<MovimientoEnemigo>().Rotar();
                    }
                    col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigoNoJugador(-dagnoGolpe);

                    Debug.Log("Enemigo Herido");
                }
            }
        }
    }
    private void ActivarAtaque()
    {
        representacionAtaque.gameObject.SetActive(true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(posicionAtaque.position, radioGolpe);
    }
}
    //void DetectarAlcance()
    //{
    //    //gameObject.GetComponent<MovimientoEnemigo>().CambiarAtacando(false);
    //    Collider2D[] areaGolpe = Physics2D.OverlapCircleAll(posicionAtaque.position, radioGolpe);
    //    foreach (Collider2D col in areaGolpe)
    //    {
    //        if (col.CompareTag("Player") || col.CompareTag("EnemigoBasico") || col.CompareTag("Invocacion") /*&& !DetectarMoviendose()*/)
    //        {
    //            enAlcance = true;
    //            //Ataque();
    //        }
    //        else
    //        {
    //            enAlcance = false;
    //        }
    //    }
    //    //Ataque();
    //}
