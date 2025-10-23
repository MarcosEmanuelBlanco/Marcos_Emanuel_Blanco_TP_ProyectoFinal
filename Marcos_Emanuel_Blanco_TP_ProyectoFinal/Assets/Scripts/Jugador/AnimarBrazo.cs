using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimarBrazo : MonoBehaviour
{
    private Animator animatorBrazo;
    [SerializeField] private GameObject dhaork;

    private void Start()
    {
        animatorBrazo = GetComponent<Animator>();
    }

    public void BrazoHabilidadActiva()
    {
        animatorBrazo.SetBool("BrazoHabilidadActiva", true);
    }

    public void BrazoHabilidadInactiva()
    {
        animatorBrazo.SetBool("BrazoHabilidadActiva", false);
    }
    private void GolpeBasico()
    {
        dhaork.GetComponent<AtaquesPrevisional>().GolpeQ();
    }

    private void GolpeCA()
    {
        dhaork.GetComponent<AtaquesPrevisional>().GolpeCA();
    }

    private void ParalizarPorCA()
    {
        dhaork.GetComponent<AtaquesPrevisional>().ParalizarPorHabilidad();
    }

    private void MovPostCA()
    {
        dhaork.GetComponent<AtaquesPrevisional>().ContinuarMovimiento();
    }
    private void ActivarEfectoCA()
    {
        dhaork.GetComponent<AtaquesPrevisional>().InicioEfectoCA();
    }

    public void CaminarAdelante()
    {
        animatorBrazo.SetBool("BrazoCaminandoAtras", false);
        animatorBrazo.SetBool("BrazoCaminandoAdelante", true);
    }

    public void CaminarAtras()
    {
        animatorBrazo.SetBool("BrazoCaminandoAdelante", false);
        animatorBrazo.SetBool("BrazoCaminandoAtras", true);
    }

    public void Quieto()
    {
        animatorBrazo.SetBool("BrazoCaminandoAtras", false);
        animatorBrazo.SetBool("BrazoCaminandoAdelante", false);
    }

    public void AnimacionGolpeBasico()
    {
        animatorBrazo.SetTrigger("BrazoGolpeBasico");
    }
    public void AnimacionGolpeCA()
    {
        animatorBrazo.SetTrigger("BrazoGolpeCA");
    }

    public void Muerte()
    {
        animatorBrazo.SetBool("BrazoMuerte",true);
    }

    public void BrazoRevivir()
    {
        animatorBrazo.SetBool("BrazoMuerte", false);
    }

    public void BrazoInerte() {
        animatorBrazo.SetBool("BrazoInerte", true);
    }

    public void BrazoActivo()
    {
        animatorBrazo.SetBool("BrazoInerte", false);
    }

    private void FrenarParaCombateFinal()
    {
        if (dhaork.GetComponent<Movimiento>().enabled == false)
        {
            Quieto();
        }
    }

    private void Update()
    {
        FrenarParaCombateFinal();
    }
}
