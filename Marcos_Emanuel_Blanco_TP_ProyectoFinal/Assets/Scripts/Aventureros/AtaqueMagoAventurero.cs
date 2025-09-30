using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueMagoAventurero : MonoBehaviour
{
    [SerializeField] private Transform representacionAtaque;
    [SerializeField] private Transform posicionAtaque;
    [SerializeField] private Vector2 areaAtaque;
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
        //animatorMov.SetBool("Persiguiendo", false);
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
        InvokeRepeating(nameof(ActivarAnimacionAtaque), 0.5f, tiempoEntreGolpes);
    }

    void Golpear()
    {
        if (DetectarMoviendose() == false && DetectarAturdido() == false && DetectarInvulnerable() == false)
        {
            Collider2D[] areaGolpe = Physics2D.OverlapBoxAll(posicionAtaque.position, areaAtaque, 0);
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

                if (col.CompareTag("Invocacion"))
                {
                    ActivarAtaque();
                    col.transform.GetComponent<Invocacion>().ModificarVidaInvocacion(-dagnoGolpe);
                    Debug.Log("Enemigo Herido");
                }

                if (col.CompareTag("EnemigoBasico"))
                {
                    ActivarAtaque();
                    //col.transform.GetComponent<MovimientoEnemigo>().CambiarAturdido(true);
                    //col.transform.GetComponent<MovimientoEnemigo>().CambiarAtqAventurero(false);
                    //col.transform.GetComponent<MovimientoEnemigo>().CambiarAtqAventurero(true);


                    //if (col.transform.position.x < transform.position.x)
                    //{
                    //    col.transform.GetComponent<MovimientoEnemigo>().Rotar();
                    //}
                    col.transform.GetComponent<MovimientoEnemigo>().CambiarAtqAventurero(true);
                    col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigoNoJugador(-dagnoGolpe);
                    //StartCoroutine(Aturdir());
                    //col.transform.GetComponent<MovimientoEnemigo>().CambiarAturdido(false);
                    //
                    Debug.Log("Enemigo Herido");
                }
            }
        }
    }

    //private IEnumerator Aturdir()
    //{
    //    yield return new WaitForSeconds(1);
    //}
    private void ActivarAtaque()
    {
        representacionAtaque.gameObject.SetActive(true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(posicionAtaque.position, areaAtaque);
    }
}
