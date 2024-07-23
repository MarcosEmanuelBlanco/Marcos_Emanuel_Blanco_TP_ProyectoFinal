using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueMina : MonoBehaviour
{
    [SerializeField] private float vidaMina;
    [SerializeField] private Transform representacionAtaque;
    [SerializeField] private Transform posicionAtaque;
    [SerializeField] private Vector2 rectGolpe;
    [SerializeField] private float dagnoGolpe;
    // Start is called before the first frame update
    void Start()
    {
        representacionAtaque.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ModificarVidaEnemigo(float puntos)
    {
        vidaMina += puntos;
        Debug.Log("Enemigo herido");
        Muerte();
    }

    private void Muerte()
    {
        if (vidaMina <= 0)
        {
            Invoke(nameof(Golpear), 0);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Aventurero") || collision.CompareTag("Invocacion"))
        {
            Invoke(nameof(Golpear), 0);
            Destroy(gameObject);
        }
    }

    void Golpear()
    {
            Collider2D[] areaGolpe = Physics2D.OverlapBoxAll(posicionAtaque.position, rectGolpe, 0);
            foreach (Collider2D col in areaGolpe)
            {
                if (col.CompareTag("Player"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoGolpe);
                    Debug.Log("Jugador Herido");
                }

                if (col.CompareTag("Aventurero"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    col.transform.GetComponent<Aventurero>().ModificarVidaEnemigoNoJugador(-dagnoGolpe);
                    Debug.Log("Enemigo Herido");
                }
            }
        
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
        Gizmos.DrawWireCube(posicionAtaque.position, rectGolpe);
    }
}
