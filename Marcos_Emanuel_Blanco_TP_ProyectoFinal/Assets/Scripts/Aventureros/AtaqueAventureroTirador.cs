using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueAventureroTirador : MonoBehaviour
{
    [SerializeField] private GameObject arma;
    [SerializeField] private Transform posicionAtaque;
    [SerializeField] private Vector2 areaAtaque;
    //[SerializeField] private bool armaActiva;
    // Start is called before the first frame update
    void Start()
    {
        //armaActiva = false;
        arma.SetActive(true);
    }

    private void OnBecameVisible()
    {
        //Disparar();
    }

    private void Disparar()
    {
        if (DetectarMoviendose() == true && DetectarAturdido() == false)
        {
            Collider2D[] areaAlcanceArma = Physics2D.OverlapBoxAll(posicionAtaque.position, areaAtaque, 0);
            foreach (Collider2D col in areaAlcanceArma)
            {
                if (col.CompareTag("Player") || col.CompareTag("EnemigoMina") || col.CompareTag("EnemigoBasico"))
                {
                    arma.SetActive(true);
                }
            }
        }else if (gameObject.GetComponent<Aventurero>().GetMoviendose() == false && gameObject.GetComponent<Aventurero>().GetAturdido() == false)
        {
            arma.SetActive(false);
        }
    }

    //private void AlternarArma()
    //{
    //    if (armaActiva)
    //    {
    //        arma.SetActive(true);
    //    }
    //    else
    //    {
    //        arma.SetActive(false);
    //    }
    //}
    private bool DetectarMoviendose()
    {
        return gameObject.GetComponent<Aventurero>().GetMoviendose();
    }

    private bool DetectarAturdido()
    {
        return gameObject.GetComponent<Aventurero>().GetAturdido();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(posicionAtaque.position, areaAtaque);
    }

    // Update is called once per frame
    void Update()
    {
        DetectarMoviendose();
        DetectarAturdido();
        Disparar();
        //AlternarArma();
    }
}
