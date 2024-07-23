using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionamientoPegote : MonoBehaviour
{
    [SerializeField] private float duracionPegote;
    [SerializeField] private float vidaPegote;
    private GameObject jugador;
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(nameof(Adhesion));
    }

    private IEnumerator Adhesion()
    {

        jugador.GetComponent<Movimiento>().CambiarAturdido(true);
        yield return new WaitForSeconds(duracionPegote);
        jugador.GetComponent<Movimiento>().CambiarAturdido(false);

    }

    public void ModificarVidaPegote(float dagno)
    {
        vidaPegote -= dagno;
        DestruirPegote();
    }

    private void DestruirPegote()
    {
        if (vidaPegote <= 0)
        {
            jugador.GetComponent<Movimiento>().CambiarAturdido(false);
            Destroy(gameObject);
        }
    }
}
