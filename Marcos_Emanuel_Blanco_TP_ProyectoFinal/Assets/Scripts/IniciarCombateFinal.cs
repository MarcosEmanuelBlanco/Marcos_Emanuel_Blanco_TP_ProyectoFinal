using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarCombateFinal : MonoBehaviour
{
    private void InicioCombate()
    {
        GameObject dhaork = GameObject.FindGameObjectWithTag("Player");
        GameObject grulgosh = GameObject.FindGameObjectWithTag("Jefe");
        dhaork.GetComponent<Movimiento>().enabled = true;
        dhaork.GetComponent<AtaquesPrevisional>().enabled = true;
        grulgosh.GetComponent<ComportamientoGulgo>().Espera();
    }
}
