using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionamientoInvArtillera : MonoBehaviour
{
    private Animator animatorArtillero;
    [SerializeField] private GameObject proyectil;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float frecuenciaDisparo;
    [SerializeField] private float velocidadInvocacion;
    private PoolProyectilInvocacion poolBolasFuego;
    // Start is called before the first frame update
    void Start()
    {
        animatorArtillero = GetComponent<Animator>();
        poolBolasFuego = GetComponent<PoolProyectilInvocacion>();
    }

    void OnBecameVisible()
    {
        Ataque();
    }

    private void FixedUpdate()
    {
        MovimientoInvocacion();
    }

    void MovimientoInvocacion()
    {
        Vector3 newPosition = transform.position + new Vector3(0.1f * velocidadInvocacion, 0f, 0f);
        transform.position = newPosition;
    }

    void Disparo()
    {
        animatorArtillero.SetTrigger("Disparar");
    }

    private void GenerarBolaFuego()
    {
        GameObject pooledBolas = poolBolasFuego.GetPooledBolasFuego();
        if (pooledBolas != null)
        {
            pooledBolas.transform.position = puntoDisparo.position;
            pooledBolas.SetActive(true);
            pooledBolas.GetComponent<FuncionamientoProyectil>().FuerzaFuego();
        }
    }

    void Ataque()
    {
        InvokeRepeating(nameof(Disparo), 0, frecuenciaDisparo);
    }
}
