using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aventurero : MonoBehaviour
{
    [SerializeField] private float vidaEnemigo;
    [SerializeField] private int valorEnemigo;
    [SerializeField] private int bonusHabilidad;
    [SerializeField] private Vector2 alcanceVision;
    [SerializeField] private float velocidadEnemigo;
    [SerializeField] private float distanciaAlJugador;
    //[SerializeField] private Transform jugador;
    private bool moviendose;
    public bool aturdido;
    // Start is called before the first frame update
    private void Start()
    {
        bonusHabilidad = Random.Range(1, 5);
        aturdido = false;
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
                jugador.GetComponent<EstadoJugador>().SumarAlmas(1);
                jugador.GetComponent<EstadoJugador>().SumarPuntos(valorEnemigo);
                AtaquesPrevisional ataques = jugador.GetComponent<AtaquesPrevisional>();
                ataques.CambiarHabilidadAleatoria(bonusHabilidad);
            }
            //GameObject[] enemigos = GameObject.FindGameObjectsWithTag("EnemigoBasico");
            //for (int i = 0; i < enemigos.Length; i++)
            //{
            //    if (enemigos[i] != null && enemigos[i].GetComponent<MovimientoEnemigo>().GetAtacado() == true)
            //    {
            //        enemigos[i].GetComponent<MovimientoEnemigo>().CambiarAtqAventurero(false);
            //    }
            //}
            Destroy(gameObject);
        }
    }

    public void ModificarVidaEnemigoNoJugador(float puntos)
    {
        vidaEnemigo += puntos;
        Debug.Log("Enemigo herido");
        MuerteNoJugador();
    }

    private void MuerteNoJugador()
    {
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }

    public bool GetMoviendose()
    {
        return moviendose;
    }

    public bool GetAturdido()
    {
        return aturdido;
    }

    public void CambiarAturdido(bool atu)
    {
        aturdido = atu;
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
                if (!aturdido)
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
            }

            if (col.CompareTag("EnemigoBasico"))
            {
                if (!aturdido)
                {
                    enemigo = col.gameObject;
                    if (enemigo != null && Mathf.Abs(transform.position.x - enemigo.transform.position.x) > distanciaAlJugador && transform.position.x < enemigo.transform.position.x && !aturdido)
                    {
                        moviendose = true;
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
                    }
                    else if (enemigo != null && Mathf.Abs(transform.position.x - enemigo.transform.position.x) > distanciaAlJugador && transform.position.x > enemigo.transform.position.x && !aturdido)
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

            if (col.CompareTag("EnemigoMina"))
            {
                if (!aturdido)
                {
                    enemigoMina = col.gameObject;
                    if (enemigoMina != null && Mathf.Abs(transform.position.x - enemigoMina.transform.position.x) > distanciaAlJugador && transform.position.x < enemigoMina.transform.position.x && !aturdido)
                    {
                        moviendose = true;
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
                    }
                    else if (enemigoMina != null && Mathf.Abs(transform.position.x - enemigoMina.transform.position.x) > distanciaAlJugador && transform.position.x > enemigoMina.transform.position.x && !aturdido)
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
