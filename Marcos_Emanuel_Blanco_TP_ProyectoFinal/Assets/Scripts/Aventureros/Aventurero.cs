using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Aventurero : MonoBehaviour
{
    [SerializeField] private Canvas barraVida;
    [SerializeField] private float vidaEnemigo;
    [SerializeField] private int valorEnemigo;
    [SerializeField] private int bonusHabilidad;
    [SerializeField] private Vector2 alcanceVision;
    [SerializeField] private float velocidadEnemigo;
    [SerializeField] private float distanciaAlJugador;
    [SerializeField] private UnityEvent<string> OnHealthChange;
    [SerializeField] private TextMeshProUGUI textoVida;
    //[SerializeField] private Transform jugador;
    [SerializeField] private bool moviendose;
    [SerializeField] private bool atacando;
    private bool aLaDerecha;
    [SerializeField] private bool aturdido;
    private Rigidbody2D rigidbody2;
    private Collider2D collider2;
    private Animator animatorMov;
    [SerializeField] private bool invulnerable;
    [SerializeField] private float duracionInvulnerabilidad;
    private void Start()
    {
        invulnerable = true;
        StartCoroutine(nameof(InvulnerabilidadInicial));
        bonusHabilidad = Random.Range(1, 5);
        OnHealthChange.Invoke(vidaEnemigo.ToString());
        aturdido = false;
        atacando = false;
        moviendose = true;
        rigidbody2 = GetComponent<Rigidbody2D>();
        collider2 = GetComponent<Collider2D>();
        animatorMov = GetComponent<Animator>();
        animatorMov.SetBool("Invulnerable", true);
    }

    //public void ActivarAnimacionMovimiento()
    //{
    //    animatorMov.SetBool("Persiguiendo", true);
    //}

    private IEnumerator InvulnerabilidadInicial()
    {
        yield return new WaitForSeconds(duracionInvulnerabilidad);
        invulnerable = false;
        animatorMov.SetBool("Invulnerable", false);
    }

    public bool GetInvulnerable()
    {
        return invulnerable;
    }

    public void ModificarVidaEnemigo(float puntos)
    {
        if (!invulnerable)
        {
            vidaEnemigo += puntos;
            OnHealthChange.Invoke(vidaEnemigo.ToString());
            Debug.Log("Enemigo herido");
            Muerte();
        }
    }

    private void Muerte()
    {
        if (vidaEnemigo <= 0)
        {
            barraVida.gameObject.SetActive(false);
            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("EnemigoBasico");
            for (int i = 0; i < enemigos.Length; i++)
            {
                if (enemigos[i] != null && enemigos[i].GetComponent<MovimientoEnemigo>().GetAtacadoPorAventurero() == true)
                {
                    enemigos[i].GetComponent<MovimientoEnemigo>().CambiarAtqAventurero(false);
                }
            }
            GameObject controlador = GameObject.FindGameObjectWithTag("GameController");
            controlador.GetComponent<GameManager>().contarDerribados();
            collider2.enabled = false;
            rigidbody2.Sleep();
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

            animatorMov.SetBool("Muerto", true);
        }
    }

    private void DestruirAventurero()
    {
        Destroy(gameObject);
    }

    public void ModificarVidaEnemigoNoJugador(float puntos)
    {
        if (!invulnerable)
        {
            vidaEnemigo += puntos;
            OnHealthChange.Invoke(vidaEnemigo.ToString());
            Debug.Log("Enemigo herido");
            MuerteNoJugador();
        }
    }

    private void MuerteNoJugador()
    {
        if (vidaEnemigo <= 0)
        {
            barraVida.gameObject.SetActive(false);
            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("EnemigoBasico");
            for(int i = 0; i < enemigos.Length; i++)
            {
                enemigos[i].GetComponent<MovimientoEnemigo>().CambiarAtqAventurero(false);
            }
            GameObject controlador = GameObject.FindGameObjectWithTag("GameController");
            controlador.GetComponent<GameManager>().contarDerribados();
            collider2.enabled = false;
            rigidbody2.Sleep();
            animatorMov.SetBool("Muerto", true);
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
                if (!aturdido && !invulnerable)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    textoVida.gameObject.transform.localScale = new(-1.0f, 1.0f, 1.0f);
                    if (Mathf.Abs(transform.position.x - jugador.transform.position.x) > distanciaAlJugador)
                    {
                        animatorMov.SetBool("Persiguiendo", true);
                        moviendose = true;
                        transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
                        aLaDerecha = true;
                    }
                    else
                    {
                        animatorMov.SetBool("Persiguiendo", false);
                        moviendose = false;
                    }
                } 
            }

            else if (col.CompareTag("EnemigoBasico") && !atacando)
            {
                if (!aturdido && !invulnerable)
                {
                    enemigo = col.gameObject;
                    if (enemigo != null && Mathf.Abs(transform.position.x - enemigo.transform.position.x) > distanciaAlJugador && transform.position.x < enemigo.transform.position.x && !aturdido)
                    {
                        animatorMov.SetBool("Persiguiendo", true);
                        moviendose = true;
                        aLaDerecha = false;
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
                        textoVida.gameObject.transform.localScale = new(1.0f,1.0f,1.0f);
                    }
                    else if (enemigo != null && Mathf.Abs(transform.position.x - enemigo.transform.position.x) > distanciaAlJugador && transform.position.x > enemigo.transform.position.x && !aturdido)
                    {
                        animatorMov.SetBool("Persiguiendo", true);
                        moviendose = true;
                        aLaDerecha = true;
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
                        textoVida.gameObject.transform.localScale = new(-1.0f, 1.0f, 1.0f);
                    }
                    else
                    {
                        animatorMov.SetBool("Persiguiendo", false);
                        moviendose = false;
                        atacando = true;
                    }
                    ////col.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoGolpe);
                    //Debug.Log("Enemigo Herido");
                } 
            }

            //else if (col.CompareTag("EnemigoMina") && !col.CompareTag("Player"))
            //{
            //    if (!aturdido)
            //    {
            //        enemigoMina = col.gameObject;
            //        if (enemigoMina != null && Mathf.Abs(transform.position.x - enemigoMina.transform.position.x) > distanciaAlJugador && transform.position.x < enemigoMina.transform.position.x && !aturdido)
            //        {
            //            animatorMov.SetBool("Persiguiendo", true);
            //            moviendose = true;
            //            aLaDerecha = false;
            //            transform.rotation = Quaternion.Euler(0, 0, 0);
            //            transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
            //        }
            //        else if (enemigoMina != null && Mathf.Abs(transform.position.x - enemigoMina.transform.position.x) > distanciaAlJugador && transform.position.x > enemigoMina.transform.position.x && !aturdido)
            //        {
            //            animatorMov.SetBool("Persiguiendo", true);
            //            moviendose = true;
            //            aLaDerecha = true;
            //            transform.rotation = Quaternion.Euler(0, 180, 0);
            //            transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
            //        }
            //        else
            //        {
            //            animatorMov.SetBool("Persiguiendo", false);
            //            moviendose = false;
            //        }
            //        ////col.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoGolpe);
            //        //Debug.Log("Enemigo Herido");
            //    }
            //}

        }
    }

    public bool HaciaDonde()
    {
        return aLaDerecha;
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
