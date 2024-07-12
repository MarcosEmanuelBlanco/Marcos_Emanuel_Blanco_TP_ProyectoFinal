using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilEnemigo : MonoBehaviour
{
    [SerializeField] private Transform posicionControladorExplosion;
    [SerializeField] private float radioExplosion;
    [SerializeField] private float dagnoExplosion;
    private Rigidbody2D rb;
    [SerializeField] private Vector2 fuerzaLanzamiento;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(fuerzaLanzamiento, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Player"))
        {
            collision.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoExplosion);
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Aventurero"))
        {
            collision.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoExplosion);
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Muro"))
        {
            collision.transform.GetComponent<FuncionamientoMuro>().dagnarMuro(-dagnoExplosion);
            gameObject.SetActive(false);
        }
    }

    //void GolpeProyectil()
    //{
    //    Collider2D[] objetos = Physics2D.OverlapCircleAll(posicionControladorExplosion.position, radioExplosion);
    //    foreach (Collider2D col in objetos)
    //    {
    //        if (col.CompareTag("Jugador"))
    //        {
    //            col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoExplosion);
    //        }

    //        if (col.CompareTag("Aventurero"))
    //        {
    //            col.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoExplosion);
    //            Debug.Log("Enemigo Herido");
    //        }
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(posicionControladorExplosion.position, radioExplosion);
    }
}
