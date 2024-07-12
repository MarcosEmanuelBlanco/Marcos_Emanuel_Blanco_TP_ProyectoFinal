using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aventurero : MonoBehaviour
{
    [SerializeField] private float vidaEnemigo;
    [SerializeField] private int bonusHabilidad;
    [SerializeField] private Vector2 alcanceVision;
    [SerializeField] private float velocidadEnemigo;
    [SerializeField] private float distanciaAlJugador;
    //[SerializeField] private Transform jugador;
    private bool moviendose;
    // Start is called before the first frame update
    private void Start()
    {
        bonusHabilidad = Random.Range(1, 9);
        
    }
    public void ModificarVidaEnemigo(float puntos)
    {
        vidaEnemigo += puntos;
        Debug.Log("Enemigo herido");
        Muerte();
    }

    private void Muerte()
    {
        if (vidaEnemigo <= 0)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                GameObject jugador = GameObject.FindGameObjectWithTag("Player");
                if (jugador == null)
                {
                    return;
                }
                AtaquesPrevisional ataques = jugador.GetComponent<AtaquesPrevisional>();
                ataques.CambiarHabilidadAleatoria(bonusHabilidad);
            }
            Destroy(gameObject);
        }
    }
    public bool GetMoviendose()
    {
        return moviendose;
    }
    void PriorizarObjetivos()
    {
        GameObject enemigo = GameObject.FindGameObjectWithTag("EnemigoBasico");
        GameObject enemigoMina = GameObject.FindGameObjectWithTag("EnemigoMina");
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        Collider2D[] areaVision = Physics2D.OverlapBoxAll(transform.position, alcanceVision,0);
        foreach (Collider2D col in areaVision)
        {
            if (col.CompareTag("Player") && enemigo == null)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                if (Mathf.Abs(transform.position.x - jugador.transform.position.x) > distanciaAlJugador)
                {
                    moviendose = true;
                    transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
                    
                }
                else
                {
                    moviendose = false;
                }
                //col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoGolpe);
                Debug.Log("Jugador Herido");
            }

            if (col.CompareTag("EnemigoBasico"))
            {
                enemigo = col.gameObject;
                if (enemigo != null && Mathf.Abs(transform.position.x - enemigo.transform.position.x) > distanciaAlJugador && transform.position.x < enemigo.transform.position.x)
                {
                    moviendose = true;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
                }
                else if (enemigo != null && Mathf.Abs(transform.position.x - enemigo.transform.position.x) > distanciaAlJugador && transform.position.x > enemigo.transform.position.x)
                {
                    moviendose = true;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
                }
                else
                {
                    moviendose = false;
                }
                //col.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoGolpe);
                Debug.Log("Enemigo Herido");
            }

            if (col.CompareTag("EnemigoMina"))
            {
                enemigoMina = col.gameObject;
                if (enemigoMina != null && Mathf.Abs(transform.position.x - enemigoMina.transform.position.x) > distanciaAlJugador && transform.position.x < enemigoMina.transform.position.x)
                {
                    moviendose = true;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
                }
                else if (enemigoMina != null && Mathf.Abs(transform.position.x - enemigoMina.transform.position.x) > distanciaAlJugador && transform.position.x > enemigoMina.transform.position.x)
                {
                    moviendose = true;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
                }
                else
                {
                    moviendose = false;
                }
                //col.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoGolpe);
                Debug.Log("Enemigo Herido");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, alcanceVision);
    }
    // Update is called once per frame
    void Update()
    {
        PriorizarObjetivos();
    }

    private void FixedUpdate()
    {
        
    }
}
