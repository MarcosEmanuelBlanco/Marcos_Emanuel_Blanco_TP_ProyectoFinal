using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueAventureroTirador : MonoBehaviour
{
    [SerializeField] private GameObject arma;
    [SerializeField] private Transform posicionAtaque;
    [SerializeField] private Vector2 areaAtaque;
    // Start is called before the first frame update
    void Start()
    {
        arma.SetActive(false);
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
        }
        else if (DetectarMoviendose() == false && DetectarAturdido() == false)
        {
            arma.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(posicionAtaque.position, areaAtaque);
    }

    private bool DetectarMoviendose()
    {
        return gameObject.GetComponent<Aventurero>().GetMoviendose();
    }

    private bool DetectarAturdido()
    {
        return gameObject.GetComponent<Aventurero>().GetAturdido();
    }

    // Update is called once per frame
    void Update()
    {
        DetectarMoviendose();
        DetectarAturdido();
        Disparar();
    }
}
