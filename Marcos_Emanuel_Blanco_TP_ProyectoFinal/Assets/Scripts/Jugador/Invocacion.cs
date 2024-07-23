using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invocacion : MonoBehaviour
{
    [SerializeField] private float vidaInvocacion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ModificarVidaEnemigo(float puntos)
    {
        vidaInvocacion += puntos;
        Debug.Log("Enemigo herido");
        Muerte();
    }

    private void Muerte()
    {
        if (vidaInvocacion <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
