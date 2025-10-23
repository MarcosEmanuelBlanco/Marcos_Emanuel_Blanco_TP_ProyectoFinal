using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FuncionamientoInvocacion : MonoBehaviour
{
    private Animator animatorSoldado;
    [SerializeField] private float velocidadInvocacion;
    [SerializeField] private Transform posicionSensorSalto;
    [SerializeField] private float alcanceSensorSalto;
    [SerializeField] private Vector2 fuerzaSalto;
    [SerializeField] private float dagnoChoque;

    private Collider2D collider2;
    private Rigidbody2D rb;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Invocacion"), LayerMask.NameToLayer("Jugador"), true);
        animatorSoldado = GetComponent<Animator>();
        collider2 = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        MovimientoInvocacion();
        RaycastHit2D rayoSensorBorde = Physics2D.Raycast(posicionSensorSalto.position, Vector2.down, alcanceSensorSalto);
        if (rayoSensorBorde.transform.CompareTag("EnemigoBasico") == true || rayoSensorBorde.transform.CompareTag("Aventurero") == true || rayoSensorBorde.transform.CompareTag("Jefe") == true || rayoSensorBorde.transform.CompareTag("EnemigoMina") == true)
        {
            Debug.Log("saltando");
            SaltoInvocacion();
        }//ajustar para evitar errores al usarlos como limpiadores de minas
    }

    void MovimientoInvocacion()
    {
        Vector3 newPosition = transform.position + new Vector3(0.1f * velocidadInvocacion, 0f, 0f);
        transform.position = newPosition;
    }

    void SaltoInvocacion()
    {
        animatorSoldado.SetTrigger("Salto");
        rb.AddForce(fuerzaSalto, ForceMode2D.Impulse);
        gameObject.GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(posicionSensorSalto.transform.position + Vector3.down * alcanceSensorSalto, posicionSensorSalto.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animatorSoldado.SetTrigger("Golpe");
        if (collision.CompareTag("EnemigoBasico"))
        {
            collision.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-dagnoChoque);
            collider2.enabled = false;
            rb.Sleep();
        }

        if (collision.CompareTag("Jefe"))
        {
            collision.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-dagnoChoque);
            collider2.enabled = false;
            rb.Sleep();
        }

        if (collision.CompareTag("Aventurero"))
        {
            collision.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoChoque);
            collider2.enabled = false;
            rb.Sleep();
        }
    }

}
