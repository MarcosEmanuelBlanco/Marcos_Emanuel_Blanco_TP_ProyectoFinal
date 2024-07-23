using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilGulgo : MonoBehaviour
{
    [SerializeField] private Transform posicionControladorExplosion;
    [SerializeField] private GameObject pegote;
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

        if (collision.CompareTag("Invocacion"))
        {
            collision.transform.GetComponent<Invocacion>().ModificarVidaEnemigo(-dagnoExplosion);
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Player"))
        {
            collision.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoExplosion);
            if(GameObject.FindGameObjectWithTag("Pegote") == null)
            {
                Instantiate(pegote,collision.transform.position += new Vector3(3.0f, 0.0f, 0.0f),Quaternion.identity);
            }
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Muro"))
        {
            collision.transform.GetComponent<FuncionamientoMuro>().DagnarMuro(-dagnoExplosion);
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(posicionControladorExplosion.position, radioExplosion);
    }
}
