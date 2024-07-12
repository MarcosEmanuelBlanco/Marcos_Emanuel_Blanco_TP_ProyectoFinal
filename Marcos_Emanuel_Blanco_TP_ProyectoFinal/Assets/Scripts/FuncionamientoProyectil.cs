using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionamientoProyectil : MonoBehaviour
{
    [SerializeField] private Transform representacionExplosion;
    [SerializeField] private Transform posicionControladorExplosion;
    [SerializeField] private float radioExplosion;
    [SerializeField] private float dagnoExplosion;
    private Rigidbody2D rb;
    [SerializeField] private Vector2 fuerzaLanzamiento;
    // Start is called before the first frame update
    void Start()
    {
        representacionExplosion.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(fuerzaLanzamiento, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo") || collision.CompareTag("EnemigoBasico") || collision.CompareTag("Aventurero"))
        {
            GolpeProyectil();
            Explotar();
        }
    }

    void Explotar()
    {
        StartCoroutine(nameof(ActivarExplosion));
    }

    private IEnumerator ActivarExplosion()
    {
        representacionExplosion.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    void GolpeProyectil()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(posicionControladorExplosion.position, radioExplosion);
        foreach (Collider2D col in objetos)
        {
            if (col.CompareTag("EnemigoBasico"))
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

    }


}
