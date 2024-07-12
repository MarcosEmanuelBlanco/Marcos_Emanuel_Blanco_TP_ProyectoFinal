using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoJugador : MonoBehaviour
{
    [SerializeField] private float vidaJugador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModificarVidaJugador(float puntos)
    {
        vidaJugador += puntos;
    }
}
