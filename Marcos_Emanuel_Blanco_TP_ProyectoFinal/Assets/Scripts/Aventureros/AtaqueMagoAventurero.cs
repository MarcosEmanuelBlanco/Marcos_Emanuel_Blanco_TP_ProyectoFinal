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
    // Start is called before the first frame update
    void Start()
    {
        representacionAtaque.gameObject.SetActive(false);

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


    // Update is called once per frame
    void Update()
    {
        DetectarMoviendose();
        DetectarAturdido();
    }

    void Ataque()
    {

        InvokeRepeating(nameof(Golpear), 0, tiempoEntreGolpes);
    }

    void Golpear()
    {
        if (DetectarMoviendose() == false && DetectarAturdido() == false)
        {
            Collider2D[] areaGolpe = Physics2D.OverlapBoxAll(posicionAtaque.position, areaAtaque, 0);
            foreach (Collider2D col in areaGolpe)
            {
                if (col.CompareTag("Player"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoGolpe);
                    Debug.Log("Jugador Herido");
                }

                if (col.CompareTag("EnemigoMina"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    col.transform.GetComponent<AtaqueMina>().ModificarVidaEnemigo(-dagnoGolpe);
                    Debug.Log("Enemigo Herido");
                }

                if (col.CompareTag("Invocacion"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    col.transform.GetComponent<Invocacion>().ModificarVidaEnemigo(-dagnoGolpe);
                    Debug.Log("Enemigo Herido");
                }

                if (col.CompareTag("EnemigoBasico"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    //col.transform.GetComponent<MovimientoEnemigo>().CambiarAturdido(true);
                    
                    col.transform.GetComponent<MovimientoEnemigo>().CambiarAtqAventurero(true);
                    //col.transform.GetComponent<MovimientoEnemigo>().CambiarAtqAventurero(false);
                    
                    if (col.transform.position.x < transform.position.x)
                    {
                        col.transform.GetComponent<MovimientoEnemigo>().Rotar();
                    }
                    col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigoNoJugador(-dagnoGolpe);
                    //StartCoroutine(Aturdir());
                    //col.transform.GetComponent<MovimientoEnemigo>().CambiarAturdido(false);
                    //
                    Debug.Log("Enemigo Herido");
                }
            }
        }
    }

    private IEnumerator Aturdir()
    {
        yield return new WaitForSeconds(1);
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
        Gizmos.DrawWireCube(posicionAtaque.position, areaAtaque);
    }
}
