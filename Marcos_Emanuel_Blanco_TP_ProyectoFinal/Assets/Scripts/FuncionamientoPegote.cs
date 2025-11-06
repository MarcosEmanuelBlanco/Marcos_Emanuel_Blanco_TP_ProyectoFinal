using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionamientoPegote : MonoBehaviour
{
    [SerializeField] private float duracionPegote;
    [SerializeField] private float vidaPegote;
    private GameObject jugador;
    private Animator animator;
    private AudioSource sonidosBloquePegote;
    [SerializeField] private AudioClip sonidoDerretimiento;
    [SerializeField] private AudioClip sonidoDestruccion;
    [SerializeField] private AudioClip sonidoImpacto;
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        sonidosBloquePegote = GetComponent<AudioSource>();
        sonidosBloquePegote.PlayOneShot(sonidoImpacto);
        StartCoroutine(nameof(Adhesion));
    }

    private IEnumerator Adhesion()
    {

        jugador.GetComponent<Movimiento>().CambiarAturdido(true);
        yield return new WaitForSeconds(duracionPegote);
        jugador.GetComponent<Movimiento>().CambiarAturdido(false);
        ExpirarPegote();

    }
    private void ExpirarPegote()
    {
        sonidosBloquePegote.PlayOneShot(sonidoDerretimiento);
        jugador.GetComponent<Movimiento>().CambiarAturdido(false);
        animator.SetTrigger("Expirar");
    }
    public void ModificarVidaPegote(float dagno)
    {
        vidaPegote += dagno;
        DagnarPegote();
    }

    private void DagnarPegote()
    {
        if (vidaPegote <= 0)
        {
            jugador.GetComponent<Movimiento>().CambiarAturdido(false);
            DestruirPegote();
        }
    }

    private void DestruirPegote()
    {
        sonidosBloquePegote.PlayOneShot(sonidoDestruccion);
        animator.SetTrigger("Destruir");
    }
    private void Destruir()
    {
        Destroy(gameObject);
    }
}
