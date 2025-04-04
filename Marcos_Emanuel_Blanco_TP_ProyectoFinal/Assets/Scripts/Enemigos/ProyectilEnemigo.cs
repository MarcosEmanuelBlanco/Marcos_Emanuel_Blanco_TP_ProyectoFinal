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
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(fuerzaLanzamiento, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            AnimarYParalizar();
        }

        if (collision.CompareTag("Player"))
        {
            AnimarYParalizar();
            collision.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoExplosion);
        }

        if(collision.CompareTag("Invocacion"))
        {
            AnimarYParalizar();
            collision.transform.GetComponent<Invocacion>().ModificarVidaEnemigo(-dagnoExplosion);
        }

        if (collision.CompareTag("Aventurero"))
        {
            AnimarYParalizar();
            collision.transform.GetComponent<Aventurero>().ModificarVidaEnemigoNoJugador(-dagnoExplosion);
        }

        if (collision.CompareTag("Muro"))
        {
            AnimarYParalizar();
            collision.transform.GetComponent<FuncionamientoMuro>().DagnarMuro(-dagnoExplosion);
        }
    }

    private void AnimarYParalizar()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetTrigger("Explosion");
    }

    private void Destruir()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        rb.rotation += 0.15f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(posicionControladorExplosion.position, radioExplosion);
    }
}
