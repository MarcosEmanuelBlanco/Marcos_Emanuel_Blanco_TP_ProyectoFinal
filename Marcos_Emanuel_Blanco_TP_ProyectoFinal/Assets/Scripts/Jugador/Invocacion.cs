using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Invocacion : MonoBehaviour
{
    [SerializeField] private float vidaInvocacion;
    private Animator animatorSoldado;
    [SerializeField] private Canvas barraVida;
    [SerializeField] private UnityEvent<string> OnHealthChange;
    [SerializeField] private TextMeshProUGUI textoVida;
    private Rigidbody2D rb;
    private Collider2D collider2;
    // Start is called before the first frame update
    void Start()
    {
        animatorSoldado = GetComponent<Animator>();
        collider2 = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        OnHealthChange.Invoke(vidaInvocacion.ToString());
    }

    public void ModificarVidaInvocacion(float puntos)
    {
        vidaInvocacion += puntos;
        OnHealthChange.Invoke(vidaInvocacion.ToString());
        MuerteInvocacion();
    }
    private void MuerteInvocacion()
    {
        if (vidaInvocacion <= 0)
        {
            animatorSoldado.SetTrigger("Muerte");
            barraVida.gameObject.SetActive(false);
            rb.Sleep();
            collider2.enabled = false;
        }
    }
    private void Desactivar()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
