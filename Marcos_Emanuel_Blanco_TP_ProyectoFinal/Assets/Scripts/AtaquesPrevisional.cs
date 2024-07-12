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
    [SerializeField] private GameObject muroR;
    [SerializeField] private Transform puntoMuro;
    [SerializeField] private float esperaSiguienteAtaqueR;
    [SerializeField] private float intervaloEntreGolpesR;
    [SerializeField] private float duracionMuroR;
    [SerializeField] private int habilidadAleatoria;
    [SerializeField] private int munW;
    [SerializeField] private int munE;
    [SerializeField] private int munR;
    // Start is called before the first frame update
    void Start()
    {
        habilidadAleatoria = 0;
        representacionAtaqueQ.gameObject.SetActive(false);
        muroR.transform.position = puntoMuro.transform.position;
        muroR.SetActive(false);
    }

    public void CambiarHabilidadAleatoria(int habAl)
    {
        habilidadAleatoria += habAl;
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

            if (col.CompareTag("EnemigoMina"))
            {
                col.transform.GetComponent<AtaqueMina>().ModificarVidaEnemigo(-dagnoGolpeQ);
                Debug.Log("Enemigo Herido");
            }

            if (col.CompareTag("Aventurero"))
            {
                col.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoGolpeQ);
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
        if (Input.GetKeyDown(KeyCode.W) && esperaSiguienteAtaqueW <= 0 && munW > 0)
        {
            esperaSiguienteAtaqueW = intervaloEntreGolpesW;
            DisparoW();
            munW--;
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
        if (Input.GetKeyDown(KeyCode.E) && esperaSiguienteAtaqueE <= 0 && munE > 0)
        {
            esperaSiguienteAtaqueE = intervaloEntreGolpesE;
            InvocarE();
            munE--;
        }
    }

    private void InvocarE()
    {
        GameObject nuevaInvocacion = grupoInvocacionesE;
        nuevaInvocacion.transform.position = puntoInvocacionE.transform.position;
        Instantiate(nuevaInvocacion);
    }

    private void UsarR()
    {
        if (esperaSiguienteAtaqueR > 0)
        {
            esperaSiguienteAtaqueR -= Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.R) && esperaSiguienteAtaqueR <= 0 && munR > 0)
        {
            esperaSiguienteAtaqueR = intervaloEntreGolpesR;
            StartCoroutine(nameof(ActivarR));
            munR--;
        }
    }

    private IEnumerator ActivarR()
    {
        muroR.SetActive(true);
        yield return new WaitForSeconds(duracionMuroR);
        muroR.SetActive(false);
        muroR.GetComponent<FuncionamientoMuro>().GolpeR();
    }

    private void UsarT()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UsarQ();
        UsarW();
        UsarE();
        UsarR();
        UsarT();
    }

}
