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
    // Update is called once per frame
    void Update()
    {

    }

    void Ataque()
    {

        InvokeRepeating(nameof(Golpear), 0, tiempoEntreGolpes);
    }

    void Golpear()
    {
        if (gameObject.GetComponent<Aventurero>().GetMoviendose() == false)
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

                if (col.CompareTag("EnemigoBasico"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    StartCoroutine(nameof(Aturdir));
                    col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-dagnoGolpe);
                    Debug.Log("Enemigo Herido");
                }
            }
        }
    }

    private IEnumerator Aturdir(Collider2D col)
    {
        col.transform.GetComponent<EnemigoPrevisional>().CambiarAtacado(true);
        yield return new WaitForSeconds(1);
        col.transform.GetComponent<EnemigoPrevisional>().CambiarAtacado(false);
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
