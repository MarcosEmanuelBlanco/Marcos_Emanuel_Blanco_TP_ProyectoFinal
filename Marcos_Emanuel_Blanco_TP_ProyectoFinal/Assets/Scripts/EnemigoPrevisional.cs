using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPrevisional : MonoBehaviour
{
    [SerializeField] private float vidaEnemigo;
    // Start is called before the first frame update

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
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
