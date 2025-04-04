using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueAreaEnemigo : MonoBehaviour
{
    [SerializeField] private Transform representacionAtaque;
    [SerializeField] private Transform posicionAtaque;
    [SerializeField] private Vector2 rectGolpe;
    [SerializeField] private float dagnoGolpe;
    [SerializeField] private float frecuenciaAtaque;
    private Animator animatorMov;
    // Start is called before the first frame update
    void Start()
    {
        representacionAtaque.gameObject.SetActive(false);
        animatorMov = GetComponent<Animator>();
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

    //void DeteccionAtaque()
    //{
    //    gameObject.GetComponent<MovimientoEnemigo>().CambiarAtacando(false);
    //    Collider2D[] areaGolpe = Physics2D.OverlapBoxAll(posicionAtaque.position, rectGolpe, 0);
    //    foreach (Collider2D col in areaGolpe)
    //    {
    //        if (col.CompareTag("Player") || col.CompareTag("Aventurero") || col.CompareTag("Invocacion"))
    //        {
    //            Ataque();
    //        }
    //    }
    //}

    void Golpear()
    {
        if (DetectarAtacando() == true && DetectarAturdido() == false)
        {
            Collider2D[] areaGolpe = Physics2D.OverlapBoxAll(posicionAtaque.position, rectGolpe, 0);
            foreach (Collider2D col in areaGolpe)
            {
                if (col.CompareTag("Player"))
                {
                    ActivarAtaque();
                    col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoGolpe);
                    Debug.Log("Jugador Herido");
                }else if (col.CompareTag("Aventurero"))
                {
                    ActivarAtaque();
                    col.transform.GetComponent<Aventurero>().ModificarVidaEnemigoNoJugador(-dagnoGolpe);
                    Debug.Log("Enemigo Herido");
                }else if (col.CompareTag("Invocacion"))
                {
                    ActivarAtaque();
                    col.transform.GetComponent<Invocacion>().ModificarVidaEnemigo(-dagnoGolpe);
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
        Gizmos.DrawWireCube(posicionAtaque.position, rectGolpe);
    }
}
