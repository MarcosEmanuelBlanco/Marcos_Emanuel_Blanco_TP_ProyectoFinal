using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionamientoProyectil : MonoBehaviour
{
    [SerializeField] private Transform posicionControladorExplosion;
    [SerializeField] private float radioExplosion;
    [SerializeField] private float dagnoExplosion;
    private float dagnoPorMultitud;
    //[SerializeField] private float bonusDagnoMultitud;
    private Rigidbody2D rb;
    private Animator miAnimator;
    [SerializeField] private Vector2 fuerzaLanzamiento;
    // Start is called before the first frame update
    void Start()
    {
        dagnoPorMultitud = 0f;
        rb = GetComponent<Rigidbody2D>();
        rb.SetRotation(-90.0f);
        miAnimator = GetComponent<Animator>();
        FuerzaFuego();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo") || collision.CompareTag("EnemigoBasico") || collision.CompareTag("Aventurero") || collision.CompareTag("Jefe"))
        {
            GolpeProyectil();
            ActivarExplosion();
        }
    }

    public void FuerzaFuego()
    {
        rb.AddForce(fuerzaLanzamiento, ForceMode2D.Impulse);
    }


    private void ActivarExplosion()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        miAnimator.Play("BolaFuegoExplosion");
    }

    void GolpeProyectil()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(posicionControladorExplosion.position, radioExplosion);
        foreach (Collider2D col in objetos)
        {
            if (col.CompareTag("EnemigoBasico"))
            {
                dagnoPorMultitud += 10;
                col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-(dagnoExplosion += dagnoPorMultitud));
            }

            if (col.CompareTag("Jefe"))
            {
                col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-dagnoExplosion);
            }

            if (col.CompareTag("Aventurero"))
            {
                col.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoExplosion);
                Debug.Log("Enemigo Herido");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posicionControladorExplosion.position, radioExplosion);
    }
    // Update is called once per frame
    void Update()
    {
        rb.rotation -= 0.10f;
    }

    private void ReiniciarRotacion()
    {
        gameObject.SetActive(false);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.SetRotation(-90.0f);
    }
}
