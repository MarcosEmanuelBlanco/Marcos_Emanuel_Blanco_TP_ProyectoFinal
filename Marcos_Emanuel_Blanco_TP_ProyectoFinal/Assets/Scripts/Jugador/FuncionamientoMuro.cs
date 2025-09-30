using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionamientoMuro : MonoBehaviour
{
    private Animator animatorMuro;
    [SerializeField] private float vidaMuro;
    [SerializeField] private float dagnoExplosionR;
    [SerializeField] private float dagnoBaseExplosionR;
    [SerializeField] private Transform puntoExplosionMuro;
    [SerializeField] private Vector2 areaExplosionMuro;
    // Start is called before the first frame update
    void Start()
    {
        animatorMuro = GetComponent<Animator>();
    }
    public void DagnarMuro(float dagnoEnemigo)
    {
        vidaMuro += dagnoEnemigo;
        dagnoExplosionR -= dagnoEnemigo;
        animatorMuro.SetTrigger("MuroImpacto");
    }

    public void GolpeR()
    {
        Collider2D[] areaGolpe = Physics2D.OverlapBoxAll(puntoExplosionMuro.position, areaExplosionMuro, 0.0f);
        foreach (Collider2D col in areaGolpe)
        {
            if (col.CompareTag("EnemigoBasico"))
            {
                col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-(dagnoBaseExplosionR + dagnoExplosionR));
                dagnoExplosionR = 0;
                Debug.Log("Enemigo Herido");
            }

            if (col.CompareTag("Jefe"))
            {
                col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-(dagnoBaseExplosionR + dagnoExplosionR));
                dagnoExplosionR = 0;
                Debug.Log("Enemigo Herido");
            }

            if (col.CompareTag("Aventurero"))
            {
                col.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-(dagnoBaseExplosionR + dagnoExplosionR));
                dagnoExplosionR = 0;
                Debug.Log("Enemigo Herido");
            }
        }
    }
    public void DestruirMuro()
    {
        animatorMuro.SetTrigger("DestruirMuro");
    }

    private void Desactivar()
    {
        gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(puntoExplosionMuro.position, areaExplosionMuro);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
