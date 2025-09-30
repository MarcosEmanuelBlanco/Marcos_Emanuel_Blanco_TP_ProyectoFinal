using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProyectilGulgo : MonoBehaviour
{
    [SerializeField] private Transform posicionControladorExplosion;
    [SerializeField] private GameObject pegote;
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
        rb.rotation = 90f;
        FuerzaEscupitajo();
    }

    public void FuerzaEscupitajo()
    {
        rb.AddForce(fuerzaLanzamiento, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            AnimarYParalizar();
        }

        if (collision.CompareTag("Invocacion"))
        {
            AnimarYParalizar();
            collision.transform.GetComponent<Invocacion>().ModificarVidaInvocacion(-dagnoExplosion);
        }

        if (collision.CompareTag("Player"))
        {
            AnimarYParalizar();
            collision.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoExplosion);
            if(GameObject.FindGameObjectWithTag("Pegote") == null)
            {
                Instantiate(pegote,collision.transform.position,Quaternion.identity);
            }
        }

        if (collision.CompareTag("Muro"))
        {
            AnimarYParalizar();
            collision.transform.GetComponent<FuncionamientoMuro>().DagnarMuro(-dagnoExplosion);
        }
    }

    private void AnimarYParalizar()
    {
        animator.SetTrigger("Explosion");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Destruir()
    {
        gameObject.SetActive(false);
        rb.constraints = RigidbodyConstraints2D.None;
    }

    public void ActivarRotacion()
    {
        rb.rotation = 90f;
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
