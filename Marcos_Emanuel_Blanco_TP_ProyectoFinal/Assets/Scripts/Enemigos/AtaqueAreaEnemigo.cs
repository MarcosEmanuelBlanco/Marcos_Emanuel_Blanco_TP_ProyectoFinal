using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueAreaEnemigo : MonoBehaviour
{
    [SerializeField] private Transform representacionAtaque;
    [SerializeField] private Transform posicionAtaque;
    [SerializeField] private Vector2 rectGolpe;
    [SerializeField] private float dagnoGolpe;
    // Start is called before the first frame update
    void Start()
    {
        representacionAtaque.gameObject.SetActive(false);

    }
    private void OnBecameVisible()
    {
        Ataque();
    }

    private bool DetectarAtacando()
    {
        return gameObject.GetComponent<MovimientoEnemigo>().GetAtacando();
    }

    private bool DetectarAturdido()
    {
        return gameObject.GetComponent<MovimientoEnemigo>().GetAturdido();
    }

    // Update is called once per frame
    void Update()
    {
        DetectarAtacando();
        DetectarAturdido();
    }

    void Ataque()
    {

        InvokeRepeating(nameof(Golpear), 0, 1.0f);
    }

    void DeteccionAtaque()
    {

    }

    void Golpear()
    {
        if (DetectarAtacando() == true && DetectarAturdido() == false)
        {
            Collider2D[] areaGolpe = Physics2D.OverlapBoxAll(posicionAtaque.position, rectGolpe, 0);
            foreach (Collider2D col in areaGolpe)
            {
                if (col.CompareTag("Player"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoGolpe);
                    Debug.Log("Jugador Herido");
                }else if (col.CompareTag("Aventurero"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    col.transform.GetComponent<Aventurero>().ModificarVidaEnemigoNoJugador(-dagnoGolpe);
                    Debug.Log("Enemigo Herido");
                }else if (col.CompareTag("Invocacion"))
                {
                    StartCoroutine(nameof(ActivarAtaque));
                    col.transform.GetComponent<Invocacion>().ModificarVidaEnemigo(-dagnoGolpe);
                }

                
            }
        }
        //else
        //{
        //    transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
        //}
    }
    private IEnumerator ActivarAtaque()
    {
        representacionAtaque.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        representacionAtaque.gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(posicionAtaque.position, rectGolpe);
    }
}
