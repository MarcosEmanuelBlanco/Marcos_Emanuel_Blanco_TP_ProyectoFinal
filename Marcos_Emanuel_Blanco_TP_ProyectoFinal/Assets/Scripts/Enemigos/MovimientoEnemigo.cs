using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    [SerializeField] private float velocidadEnemigo;
    [SerializeField] private float distanciaAlJugador;
    [SerializeField] private float distanciaAlAventurero;
    [SerializeField] private Transform puntoVision;
    public bool atacando;
    public bool aturdido;
    //[SerializeField] private float posicionABS;
    [SerializeField] private bool atacadoPorAventurero;
    public int rotacion;
    [SerializeField] private Vector2 alcanceVision;
    [SerializeField] private TextMeshProUGUI textoVida;
    private Animator animatorMov;
    void Start()
    {
        atacando = false;
        aturdido = false;
        //representacionAtaque.gameObject.SetActive(false);
        atacadoPorAventurero = false;
        animatorMov = GetComponent<Animator>();

    }



    public void ActivarAnimacionAturdimiento()
    {
        animatorMov.SetBool("Aturdimiento", true);
    }

    public void DesactivarAnimacionAturdimiento()
    {
        animatorMov.SetBool("Aturdimiento", false);
    }

    public void ActivarAnimacionMovimiento()
    {
        animatorMov.SetBool("Persiguiendo", true);
    }

    private void OnBecameVisible()
    {

        //Ataque();
    }

    void Update()
    {
        Movimiento();
        //DetectarABS();
        CambiarMoviendose();
    }

    private void FixedUpdate()
    {

    }

    void Movimiento()
    {
        Collider2D[] areaVision = Physics2D.OverlapBoxAll(puntoVision.position, alcanceVision, 0);
        foreach (Collider2D col in areaVision)
        {
            if (!atacadoPorAventurero && col.CompareTag("Player"))
            {
                Invoke(nameof(MovimientoHaciaJugador),0);
                CancelInvoke(nameof(MovimientoHaciaAventurero));
                
            }
            if (atacadoPorAventurero && col.CompareTag("Aventurero"))
            {
                Invoke(nameof(MovimientoHaciaAventurero),0);
                CancelInvoke(nameof(MovimientoHaciaJugador));
            }
        }
        if (!DetectarAventureros())
        {
            rotacion = 0;
            Rotar();
            
        }
    }

    //private void DetectarABS()
    //{
    //    GameObject aventurero = GameObject.FindGameObjectWithTag("Aventurero");
    //    posicionABS = Mathf.Abs(transform.position.x - aventurero.transform.position.x);
    //}

    bool DetectarAventureros()
    {
        if (!GameObject.FindGameObjectWithTag("Aventurero"))
        {
            return false;
        }
        return true;
    }
    void MovimientoHaciaAventurero()
    {
        GameObject aventurero = GameObject.FindGameObjectWithTag("Aventurero");
        if(!aturdido)
        {
            
            if(aventurero != null)
            {
                if (Mathf.Abs(transform.position.x - aventurero.transform.position.x) > distanciaAlAventurero && transform.position.x > aventurero.transform.position.x && !aturdido)
                {
                    animatorMov.SetBool("Persiguiendo", true);
                    textoVida.gameObject.transform.localScale = new(1.0f, 1.0f, 1.0f);
                    transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.left);
                }
                else if (Mathf.Abs(transform.position.x - aventurero.transform.position.x) < distanciaAlAventurero && transform.position.x > aventurero.transform.position.x && !aturdido)
                {
                    animatorMov.SetBool("Persiguiendo", true);
                    if(textoVida.gameObject.transform.localScale.x != 1.0f)
                    {
                        textoVida.gameObject.transform.localScale = new(1.0f, 1.0f, 1.0f);
                    }
                    transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.right);
                }
                else if(/*2.0f > Mathf.Abs(transform.position.x - aventurero.transform.position.x) &&*/ Mathf.Abs(transform.position.x - aventurero.transform.position.x)/*posicionABS*/ <= distanciaAlAventurero && !aturdido)
                {
                    atacando = true;
                    animatorMov.SetBool("Persiguiendo", false);
                }
            }
            //else
            //{
            //    //Rotar();
                
            //}
            
        }
        //if (Mathf.Abs(transform.position.x - aventurero.transform.position.x) > distanciaAlAventurero && !aturdido && atacando)
        //{
        //    animatorMov.SetBool("Persiguiendo", false);
        //    atacando = false;
        //}
    }

    public void Rotar()
    {
        transform.SetLocalPositionAndRotation(transform.position, Quaternion.Euler(0, rotacion, 0));
    }

    //void RotarIzquierda()
    //{
    //    transform.Rotate(0, 0, 0);
    //}
    void MovimientoHaciaJugador()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");        
        if (!aturdido && !atacando)
        {

            if (Mathf.Abs(transform.position.x - jugador.transform.position.x) > distanciaAlJugador && !aturdido)
            {
                animatorMov.SetBool("Persiguiendo", true);
                transform.Translate(Time.deltaTime * velocidadEnemigo * Vector2.left);
                textoVida.gameObject.transform.localScale = new(1.0f, 1.0f, 1.0f);
                //CambiarAtacando(false);

            }
            else if (Mathf.Abs(transform.position.x - jugador.transform.position.x) <= distanciaAlJugador && !aturdido)
            {
                atacando = true;
                animatorMov.SetBool("Persiguiendo", false);
            }
        }
        if (Mathf.Abs(transform.position.x - jugador.transform.position.x) > distanciaAlJugador && !aturdido && atacando)
        {
            atacando = false;
            animatorMov.SetBool("Persiguiendo", false);
        }
    }
    public bool GetAtacando()
    {
        return atacando;
    }

    public bool GetAturdido()
    {
        return aturdido;
    }

    public void CambiarAturdido(bool atu)
    {
        aturdido = atu;
    }

    public void CambiarAtacando(bool atacandox)
    {
        atacando = atacandox;
    }
    public float GetDistancia()
    {
        return distanciaAlJugador;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(puntoVision.position, alcanceVision);
    }
    public void CambiarMoviendose()
    {
        //if (atacadoPorAventurero)
        //{
        //    atacando = true;
        //}
        //else { atacando = false; }
    }
    public bool GetAtacadoPorAventurero()
    {
        return atacadoPorAventurero;
    }

    public void CambiarAtqAventurero(bool atq)
    {
        atacadoPorAventurero = atq;
    }
}
