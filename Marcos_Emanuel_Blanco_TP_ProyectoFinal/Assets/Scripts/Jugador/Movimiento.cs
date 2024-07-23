using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private float moverHorizontal;
    private float direccion;
    //private bool orientacionDer = true;
    [SerializeField] private int velocidad;
    [SerializeField] private Transform limiteIzquierdo;
    [SerializeField] private Transform limiteDerecho;
    private bool paralizado;
    // Start is called before the first frame update
    void Start()
    {
        paralizado = false;
    }

    public void CambiarAturdido(bool atu)
    {
        paralizado = atu;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paralizado)
        {
            moverHorizontal = Input.GetAxis("Horizontal");
            direccion = moverHorizontal;
            //FlipHorizontal();
        }
        
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = transform.position + new Vector3(0.1f * direccion, 0f, 0f);
        newPosition.x = Mathf.Clamp(newPosition.x, limiteIzquierdo.position.x, limiteDerecho.position.x);
        transform.position = newPosition;
    }

    //private void FlipHorizontal()
    //{
    //    //if ((orientacionDer == true && moverHorizontal < 0f) || (orientacionDer == false && moverHorizontal > 0f))
    //    //{
    //    //    orientacionDer = !orientacionDer;
    //    //    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    //    //}
    //}

}
