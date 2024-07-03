using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquesPrevisional : MonoBehaviour
{
    [SerializeField] private Transform representacionAtaqueQ;
    [SerializeField] private Transform posicionControladorGolpeQ;
    [SerializeField] private float esperaSiguienteAtaqueQ;
    [SerializeField] private float intervaloEntreGolpesQ;
    [SerializeField] private float radioGolpeQ;
    [SerializeField] private float dagnoGolpeQ;
    [SerializeField] private GameObject proyectilW;
    [SerializeField] private Transform puntoDisparoW;
    [SerializeField] private float esperaSiguienteAtaqueW;
    [SerializeField] private float intervaloEntreGolpesW;
    [SerializeField] private GameObject grupoInvocacionesE;
    [SerializeField] private Transform puntoInvocacionE;
    [SerializeField] private float esperaSiguienteAtaqueE;
    [SerializeField] private float intervaloEntreGolpesE;
    // Start is called before the first frame update
    void Start()
    {
        representacionAtaqueQ.gameObject.SetActive(false);
    }

    private void UsarQ()
    {
        if (esperaSiguienteAtaqueQ > 0)
        {
            esperaSiguienteAtaqueQ -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Q) && esperaSiguienteAtaqueQ <= 0)
        {
            esperaSiguienteAtaqueQ = intervaloEntreGolpesQ;
            GolpeQ();
            StartCoroutine(nameof(ActivarAtaqueQ));
        }
    }

    private IEnumerator ActivarAtaqueQ()
    {
        representacionAtaqueQ.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        representacionAtaqueQ.gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(posicionControladorGolpeQ.position, radioGolpeQ);
    }
    private void GolpeQ()
    {
        Collider2D[] areaGolpe = Physics2D.OverlapCircleAll(posicionControladorGolpeQ.position, radioGolpeQ);
        foreach (Collider2D col in areaGolpe)
        {
            if (col.CompareTag("EnemigoBasico"))
            {
                col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-dagnoGolpeQ);
                Debug.Log("Enemigo Herido");
            }
        }
    }

    private void UsarW()
    {
        if (esperaSiguienteAtaqueW > 0)
        {
            esperaSiguienteAtaqueW -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.W) && esperaSiguienteAtaqueW <= 0)
        {
            esperaSiguienteAtaqueW = intervaloEntreGolpesW;
            DisparoW();
        }
    }

    private void DisparoW()
    {
        GameObject nuevoProyectil = proyectilW;
        nuevoProyectil.transform.position = puntoDisparoW.transform.position;
        Instantiate(nuevoProyectil);
    }

    private void UsarE()
    {
        if (esperaSiguienteAtaqueE > 0)
        {
            esperaSiguienteAtaqueE -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.E) && esperaSiguienteAtaqueE <= 0)
        {
            esperaSiguienteAtaqueE = intervaloEntreGolpesE;
            InvocarE();
        }
    }

    private void InvocarE()
    {
        GameObject nuevaInvocacion = grupoInvocacionesE;
        nuevaInvocacion.transform.position = puntoInvocacionE.transform.position;
        Instantiate(nuevaInvocacion);
    }
    // Update is called once per frame
    void Update()
    {
        UsarQ();
        UsarW();
        UsarE();
    }

}
