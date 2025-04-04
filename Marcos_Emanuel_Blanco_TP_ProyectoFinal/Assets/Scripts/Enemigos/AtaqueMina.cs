using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueMina : MonoBehaviour
{
    [SerializeField] private float vidaMina;
    [SerializeField] private Transform posicionAtaque;
    [SerializeField] private Vector2 rectGolpe;
    [SerializeField] private float dagnoGolpe;
    [SerializeField] private bool contacto;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        contacto = false;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Muerte();
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
            Invoke(nameof(AnimacionExplosion), 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))/*Al parecer a ninguno*/
        {
            vidaMina = 0;
            contacto = true;
            //Muerte();
        }
        else if (collision.CompareTag("Aventurero"))
        {
            vidaMina = 0;
            //Muerte();
        }
        else if (collision.CompareTag("Invocacion"))
        {
            vidaMina = 0;
            //Muerte();
        }
    }

    private void AnimacionExplosion()
    {
        animator.SetTrigger("Explosion");
    }

    private void Destruir()
    {
        Destroy(gameObject);
    }

    void Golpear()
    {
            Collider2D[] areaGolpe = Physics2D.OverlapBoxAll(posicionAtaque.position, rectGolpe, 0);
            foreach (Collider2D col in areaGolpe)
            {
                if (col.CompareTag("Player"))
                {
                    col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoGolpe);
                    Debug.Log("Jugador Herido");
                }

                if (col.CompareTag("Aventurero"))
                {
                    col.transform.GetComponent<Aventurero>().ModificarVidaEnemigoNoJugador(-dagnoGolpe);
                    Debug.Log("Enemigo Herido");
                }
            }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(posicionAtaque.position, rectGolpe);
    }
}
