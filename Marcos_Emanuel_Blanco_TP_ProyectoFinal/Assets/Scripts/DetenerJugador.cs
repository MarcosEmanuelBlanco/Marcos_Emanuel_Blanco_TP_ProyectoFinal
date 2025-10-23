using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetenerJugador : MonoBehaviour
{
    private void EsperaCombate()
    {
        GameObject dhaork = GameObject.FindGameObjectWithTag("Player");
        if (dhaork.GetComponent<Movimiento>().enabled == true)
        {
            dhaork.GetComponent<Movimiento>().enabled = false;
        }
        if (dhaork.GetComponent<AtaquesPrevisional>().enabled == true)
        {
            dhaork.GetComponent<AtaquesPrevisional>().enabled = false;
        }
    }
}
