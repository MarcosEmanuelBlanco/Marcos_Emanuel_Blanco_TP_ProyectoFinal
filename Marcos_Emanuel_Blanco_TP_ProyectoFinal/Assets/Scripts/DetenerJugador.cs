using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetenerJugador : MonoBehaviour
{
    [SerializeField] private GameObject manager;
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

    public void Omitir()
    {
        GameObject dhaork = GameObject.FindGameObjectWithTag("Player");
        GameObject grulgosh = GameObject.FindGameObjectWithTag("Jefe");
        dhaork.GetComponent<Movimiento>().enabled = true;
        dhaork.GetComponent<AtaquesPrevisional>().enabled = true;
        grulgosh.GetComponent<ComportamientoGulgo>().Espera();
        gameObject.SetActive(false);
        manager.GetComponent<GameManager>().ReproducirTemaJefe();
    }
}
