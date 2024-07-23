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
        //arma.SetActive(false);
    }

    private void OnBecameVisible()
    {
        Disparar();
    }

    private void Disparar()
    {
        if (gameObject.GetComponent<Aventurero>().GetMoviendose() == false && gameObject.GetComponent<Aventurero>().GetAturdido() == false)
        {
            Collider2D[] areaAlcanceArma = Physics2D.OverlapBoxAll(posicionAtaque.position, areaAtaque, 0);
            foreach (Collider2D col in areaAlcanceArma)
            {
                if (col.CompareTag("Player"))
                {
                    arma.SetActive(true);
                }

                if (col.CompareTag("EnemigoMina"))
                {
                    arma.SetActive(true);
                }

                if (col.CompareTag("EnemigoBasico"))
                {
                    arma.SetActive(true);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(posicionAtaque.position, areaAtaque);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
