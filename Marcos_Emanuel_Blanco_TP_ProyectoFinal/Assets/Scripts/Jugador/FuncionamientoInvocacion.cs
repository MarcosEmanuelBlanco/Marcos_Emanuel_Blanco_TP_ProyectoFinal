using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionamientoInvocacion : MonoBehaviour
{

    [SerializeField] private float velocidadInvocacion;
    [SerializeField] private Transform posicionSensorSalto;
    [SerializeField] private float alcanceSensorSalto;
    [SerializeField] private Vector2 fuerzaSalto;
    [SerializeField] private float dagnoChoque;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovimientoInvocacion();
        RaycastHit2D rayoSensorBorde = Physics2D.Raycast(posicionSensorSalto.position, Vector2.down, alcanceSensorSalto);
        if (rayoSensorBorde.transform.CompareTag("EnemigoBasico") == true || rayoSensorBorde.transform.CompareTag("Aventurero") == true || rayoSensorBorde.transform.CompareTag("Jefe") == true)
        {
            Debug.Log("saltando");
            SaltoInvocacion();
        }
    }

    void MovimientoInvocacion()
    {
        Vector3 newPosition = transform.position + new Vector3(0.1f * velocidadInvocacion, 0f, 0f);
        transform.position = newPosition;
    }

    void SaltoInvocacion()
    {
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
        if (collision.CompareTag("EnemigoBasico"))
        {
            collision.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-dagnoChoque);
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Jefe"))
        {
            collision.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-dagnoChoque);
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Aventurero"))
        {
            collision.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoChoque);
            gameObject.SetActive(false);
            Debug.Log("Enemigo Herido");
        }
    }

}
