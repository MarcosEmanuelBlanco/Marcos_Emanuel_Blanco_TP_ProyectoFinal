using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimarPiernas : MonoBehaviour
{
    private Animator animatorPiernas;
    // Start is called before the first frame update
    void Start()
    {
        animatorPiernas = GetComponent<Animator>();
    }

    public void CaminarAdelante()
    {
        animatorPiernas.SetBool("PiernasCaminandoAtras", false);
        animatorPiernas.SetBool("PiernasCaminandoAdelante", true);
    }

    public void CaminarAtras()
    {
        animatorPiernas.SetBool("PiernasCaminandoAdelante", false);
        animatorPiernas.SetBool("PiernasCaminandoAtras", true);
    }

    public void Quieto()
    {
        animatorPiernas.SetBool("PiernasCaminandoAtras", false);
        animatorPiernas.SetBool("PiernasCaminandoAdelante", false);
    }

    private void FrenarParaCombateFinal()
    {
        GameObject dhaork = GameObject.FindGameObjectWithTag("Player");
        if (dhaork.GetComponent<Movimiento>().enabled == false)
        {
            Quieto();
        }
    }

    // Update is called once per frame
    void Update()
    {
        FrenarParaCombateFinal();
    }
}
