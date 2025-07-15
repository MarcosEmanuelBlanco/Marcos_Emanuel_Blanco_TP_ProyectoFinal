using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaAventurero : MonoBehaviour
{
    [SerializeField] private Transform posicionControladorDagno;
    [SerializeField] private float areaBala;
    [SerializeField] private float dagnoBala;
    private Rigidbody2D rb;
    //private Animator miAnimator;
    [SerializeField] private Vector2 fuerzaLanzamiento;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CambiarDireccionBala();
        //miAnimator = GetComponent<Animator>();
        FuerzaDisparo();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemigoBasico") || collision.CompareTag("Player") || collision.CompareTag("Invocacion"))
        {
            GolpeProyectil();
            //Explotar();
        }
    }

    public void FuerzaDisparo()
    {
        rb.AddForce(fuerzaLanzamiento, ForceMode2D.Impulse);
    }

    //void Explotar()
    //{
    //    StartCoroutine(nameof(ActivarExplosion));
    //}

    //private IEnumerator ActivarExplosion()
    //{
    //    miAnimator.Play("BolaFuegoExplosion");
    //    //rb.constraints = RigidbodyConstraints2D.FreezeAll;
    //    yield return new WaitForSeconds(0.25f);
    //    Destroy(gameObject);
    //}

    void GolpeProyectil()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(posicionControladorDagno.position, areaBala);
        foreach (Collider2D col in objetos)
        {
            if (col.CompareTag("EnemigoBasico"))
            {
                col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-dagnoBala);
                gameObject.SetActive(false);
            }

            if (col.CompareTag("Player"))
            {
                col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoBala);
                gameObject.SetActive(false);
            }

            //if (col.CompareTag("Invocacion"))
            //{
            //    col.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoBala);
            //    Destroy(gameObject);
            //}
        }
    }

    public void CambiarDireccionBala()
    {
        GameObject aventurero = GameObject.FindGameObjectWithTag("Aventurero");
        
        if (aventurero.GetComponent<Aventurero>().HaciaDonde())
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            fuerzaLanzamiento = -fuerzaLanzamiento;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            fuerzaLanzamiento = fuerzaLanzamiento;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posicionControladorDagno.position, areaBala);
    }
}
