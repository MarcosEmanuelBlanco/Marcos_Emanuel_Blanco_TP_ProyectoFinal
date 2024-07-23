using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueCACEnemigo : MonoBehaviour
{
    [SerializeField] private Transform representacionAtaque;
    [SerializeField] private Transform posicionAtaque;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dagnoGolpe;
    // Start is called before the first frame update
    void Start()
    {
        representacionAtaque.gameObject.SetActive(false);
        
    }
    private void OnBecameVisible()
    {
        /*Deteccion*/Ataque();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Ataque()
    {

        InvokeRepeating(nameof(Golpear), 0, 1.0f);
    }

    void DeteccionAtaque()
    {
        Collider2D[] areaGolpe = Physics2D.OverlapCircleAll(posicionAtaque.position, radioGolpe);
        foreach (Collider2D col in areaGolpe)
        {
            if(col.CompareTag("Player") || col.CompareTag("Aventurero") || col.CompareTag("Invocacion"))
            {
                Ataque();
            }
        }
    }

    void Golpear()
    {
        //if (gameObject.GetComponent<MovimientoEnemigo>().GetMoviendose() == true && gameObject.GetComponent<MovimientoEnemigo>().GetAturdido() == false)
        //{
            Collider2D[] areaGolpe = Physics2D.OverlapCircleAll(posicionAtaque.position, radioGolpe);
            foreach (Collider2D col in areaGolpe)
            {
                if (col.CompareTag("Player"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoGolpe);
                    Debug.Log("Jugador Herido");
                }else if (col.CompareTag("Aventurero"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    col.transform.GetComponent<Aventurero>().ModificarVidaEnemigoNoJugador(-dagnoGolpe);
                    Debug.Log("Enemigo Herido");
                }else if (col.CompareTag("Invocacion"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    col.transform.GetComponent<Invocacion>().ModificarVidaEnemigo(-dagnoGolpe);
                }


            }
        //}
        //else
        //{
        //    
        //}
    }
    private IEnumerator ActivarAtaque()
    {
        representacionAtaque.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        representacionAtaque.gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(posicionAtaque.position, radioGolpe);
    }
}
