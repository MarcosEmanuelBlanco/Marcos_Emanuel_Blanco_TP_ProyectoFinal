using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private float moverHorizontal;
    private float direccion;
    [SerializeField] private GameObject cuerpo;
    [SerializeField] private float velocidad;
    [SerializeField] private Transform limiteIzquierdo;
    [SerializeField] private Transform limiteDerecho;
    [SerializeField] private GameObject piernas;
    [SerializeField] private GameObject brazo;
    private Animator animatorCuerpo;
    private bool paralizado;
    void Start()
    {
        paralizado = false;
        animatorCuerpo = GetComponent<Animator>();
    }

    public void CambiarAturdido(bool atu)
    {
        paralizado = atu;
    }

    public bool GetAturdido()
    {
        return paralizado;
    }

    private void OnDisable()
    {
        animatorCuerpo.SetBool("CaminandoAdelante", false);
        animatorCuerpo.SetBool("CaminandoAtras", false);
    }

    void Update()
    {
        if (!paralizado)
        {
            moverHorizontal = Input.GetAxis("Horizontal") * velocidad;
            direccion = moverHorizontal;
            if(moverHorizontal > 0)
            {
                animatorCuerpo.SetBool("CaminandoAdelante", true);
                animatorCuerpo.SetBool("CaminandoAtras", false);
                brazo.GetComponent<AnimarBrazo>().CaminarAdelante();
                piernas.GetComponent<AnimarPiernas>().CaminarAdelante();
            }
            if (moverHorizontal < 0)
            {
                animatorCuerpo.SetBool("CaminandoAdelante", false);
                animatorCuerpo.SetBool("CaminandoAtras", true);
                brazo.GetComponent<AnimarBrazo>().CaminarAtras();
                piernas.GetComponent<AnimarPiernas>().CaminarAtras();
            }
            if (moverHorizontal == 0)
            {
                animatorCuerpo.SetBool("CaminandoAdelante", false);
                animatorCuerpo.SetBool("CaminandoAtras", false);
                brazo.GetComponent<AnimarBrazo>().Quieto();
                piernas.GetComponent<AnimarPiernas>().Quieto();
            }
        }

        if (paralizado)
        {
            animatorCuerpo.SetBool("CaminandoAdelante", false);
            animatorCuerpo.SetBool("CaminandoAtras", false);
        }
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = transform.position + new Vector3(0.1f * direccion, 0f, 0f);
        newPosition.x = Mathf.Clamp(newPosition.x, limiteIzquierdo.position.x, limiteDerecho.position.x);
        transform.position = newPosition;
    }
}
