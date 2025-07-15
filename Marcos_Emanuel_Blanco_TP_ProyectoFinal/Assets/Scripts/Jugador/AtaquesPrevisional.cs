using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AtaquesPrevisional : MonoBehaviour
{
    [Header("Q")]

    [SerializeField] private Transform representacionAtaqueQ;
    [SerializeField] private Transform posicionControladorGolpeQ;
    [SerializeField] private float esperaSiguienteAtaqueQ;
    [SerializeField] private float intervaloEntreGolpesQ;
    [SerializeField] private float radioGolpeQ;
    [SerializeField] private float dagnoGolpeQ;

    [Header("W")]

    [SerializeField] private GameObject proyectilW;
    [SerializeField] private Transform puntoDisparoW;
    [SerializeField] private float esperaSiguienteAtaqueW;
    [SerializeField] private float intervaloEntreGolpesW;
    private PoolProyectilW poolBolasInfernales;

    [Header("E")]

    [SerializeField] private GameObject[] grupoInvocacionesE;
    [SerializeField] private Transform[] puntoInvocacionE;
    [SerializeField] private float esperaSiguienteAtaqueE;
    [SerializeField] private float intervaloEntreGolpesE;

    [Header("R")]

    [SerializeField] private GameObject muroR;
    [SerializeField] private Transform puntoMuro;
    [SerializeField] private Transform explosionMuro;
    [SerializeField] private float esperaSiguienteAtaqueR;
    [SerializeField] private float intervaloEntreGolpesR;
    [SerializeField] private float duracionMuroR;

    [Header("Aleatoria")]

    [SerializeField] private int habilidadAleatoria;
    //private bool aleatoriaDisponible;

    [Header("Corte Aplastante")]

    [SerializeField] private Transform representacionAtaqueCA;
    [SerializeField] private Transform posicionControladorGolpeCA;
    [SerializeField] private Vector2 areaGolpeCA;
    [SerializeField] private float dagnoGolpeCA;
    [SerializeField] private Vector2 fuerzaGolpeCA;

    [Header("Rel�mpago")]

    [SerializeField] private Transform representacionPreparacionRE;
    [SerializeField] private Transform representacionAtaqueRE;
    [SerializeField] private Transform posicionControladorGolpeRE;
    [SerializeField] private Vector2 areaGolpeRE;
    [SerializeField] private float dagnoGolpeRE;
    [SerializeField] private float duracionAturdimientoRE;
    [SerializeField] private float tiempoCargaRE;

    [Header("Invocar Artiller�a")]

    [SerializeField] private GameObject[] invocacionArtilleria;
    [SerializeField] private Transform[] puntoArtilleria;

    [Header("Municiones")]

    [SerializeField] private UnityEvent<string> OnWAmmoChange;
    [SerializeField] private int munW;
    private int munWActual;
    [SerializeField] private UnityEvent<string> OnEAmmoChange;
    [SerializeField] private int munE;
    private int munEActual;
    [SerializeField] private UnityEvent<string> OnRAmmoChange;
    [SerializeField] private int munR;
    private int munRActual;
    [SerializeField] private UnityEvent<string> OnRandomSkillChange;
    // Start is called before the first frame update
    void Start()
    {
        //aleatoriaDisponible = false;
        representacionAtaqueQ.gameObject.SetActive(false);
        explosionMuro.gameObject.SetActive(false);
        representacionAtaqueCA.gameObject.SetActive(false);
        representacionAtaqueRE.gameObject.SetActive(false);
        representacionPreparacionRE.gameObject.SetActive(false);
        for (int i = 0; i < invocacionArtilleria.Length; i++)
        {
            
            //invocacionArtilleria[i].SetActive(false);
        }
        muroR.transform.position = puntoMuro.transform.position;
        muroR.SetActive(false);
        munWActual = munW;
        munEActual = munE;
        munRActual = munR;
        OnWAmmoChange.Invoke(munWActual.ToString());
        OnEAmmoChange.Invoke(munEActual.ToString());
        OnRAmmoChange.Invoke(munRActual.ToString());
        OnRandomSkillChange.Invoke(" ");
        poolBolasInfernales = GetComponent<PoolProyectilW>();
    }

    public void CambiarHabilidadAleatoria(int habAl)
    {
        habilidadAleatoria += habAl;
        //aleatoriaDisponible = true;
        switch (habilidadAleatoria)
        {
            case 1:
                OnRandomSkillChange.Invoke("Corte Aplastante");
                break;
            case 2:
                OnRandomSkillChange.Invoke("Rel�mpago");
                break;
            case 3:
                OnRandomSkillChange.Invoke("Recarga");
                break;
            case 4:
                OnRandomSkillChange.Invoke("Invocar Artiller�a");
                break;
        }
    }
    private void UsarQ()
    {
        esperaSiguienteAtaqueQ = intervaloEntreGolpesQ;
        GolpeQ();
        StartCoroutine(nameof(ActivarAtaqueQ));
        
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(posicionControladorGolpeCA.position, areaGolpeCA);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(posicionControladorGolpeRE.position, areaGolpeRE);
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

            if (col.CompareTag("Pegote"))
            {
                col.transform.GetComponent<FuncionamientoPegote>().ModificarVidaPegote(-dagnoGolpeQ);
                Debug.Log("Enemigo Herido");
            }
        }
    }

    private void UsarW()
    {
        munWActual--;
        OnWAmmoChange.Invoke(munWActual.ToString());
        esperaSiguienteAtaqueW = intervaloEntreGolpesW;
        DisparoW();
        
    }

    private void DisparoW()
    {
        //GameObject nuevoProyectil = proyectilW;
        //nuevoProyectil.transform.position = puntoDisparoW.transform.position;
        //Instantiate(nuevoProyectil);
        GenerarBolaInfernal();
    }

    private void GenerarBolaInfernal()
    {
        GameObject pooledBolasInfernales = poolBolasInfernales.GetPooledBolasInfernales();
        if (pooledBolasInfernales != null)
        {
            pooledBolasInfernales.transform.position = puntoDisparoW.position;
            pooledBolasInfernales.SetActive(true);
            pooledBolasInfernales.GetComponent<FuncionamientoProyectil>().FuerzaFuego();
        }
    }

    private void UsarE()
    {
        munEActual--;
        OnEAmmoChange.Invoke(munEActual.ToString());
        esperaSiguienteAtaqueE = intervaloEntreGolpesE;
        InvocarE();
        
    }

    private void InvocarE()
    {
        for(int i = 0; i< grupoInvocacionesE.Length; i++)
        {
            GameObject nuevaInvocacion = grupoInvocacionesE[i];
            nuevaInvocacion.transform.position = puntoInvocacionE[i].transform.position;
            Instantiate(nuevaInvocacion);
        }
        
    }

    private void UsarR()
    {
        munRActual--;
        OnRAmmoChange.Invoke(munRActual.ToString());
        esperaSiguienteAtaqueR = intervaloEntreGolpesR;
        StartCoroutine(nameof(ActivarR));
        
    }

    private IEnumerator ActivarR()
    {
        muroR.SetActive(true);
        yield return new WaitForSeconds(duracionMuroR);
        StartCoroutine(nameof(ExplosionMuro));
        muroR.SetActive(false);
        muroR.GetComponent<FuncionamientoMuro>().GolpeR();
    }

    private IEnumerator ExplosionMuro()
    {
        explosionMuro.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        explosionMuro.gameObject.SetActive(false);
    }

    private void UsarT()
    {
        //while (aleatoriaDisponible)
        //{
            switch(habilidadAleatoria)
            {
                case 1:
                    CorteAplastante();
                    OnRandomSkillChange.Invoke("Ninguna");
                    habilidadAleatoria = 0;
                    //aleatoriaDisponible = false;
                    break;
                case 2:
                    Relampago();
                    OnRandomSkillChange.Invoke("Ninguna");
                    habilidadAleatoria = 0;
                    //aleatoriaDisponible = false;                    
                    break;
                case 3:
                    RecuperarMunicion();
                    OnRandomSkillChange.Invoke("Ninguna");
                    habilidadAleatoria = 0;
                    //aleatoriaDisponible = false;
                    break;
                case 4:
                    InvocarArtilleria();
                    OnRandomSkillChange.Invoke("Ninguna");
                    habilidadAleatoria = 0;
                    //aleatoriaDisponible = false;
                    break;
                default:
                    break;
            }
        //}
    }

    private void InvocarArtilleria()
    {
        for (int i = 0; i < invocacionArtilleria.Length; i++)
        {
            GameObject nuevaArtilleria = invocacionArtilleria[i];
            nuevaArtilleria.transform.position = puntoArtilleria[i].transform.position;
            Instantiate(nuevaArtilleria);
        }
    }

    private void RecuperarMunicion()
    {
        munWActual = munW;
        OnWAmmoChange.Invoke(munWActual.ToString());
        munEActual = munE;
        OnEAmmoChange.Invoke(munEActual.ToString());
        munRActual = munR;
        OnRAmmoChange.Invoke(munRActual.ToString());
        //StartCoroutine(nameof(ParpadeoMunicion));
    }

    //private IEnumerator ParpadeoMunicion()
    //{
    //    gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    //    yield return new WaitForSeconds(0.1f);
    //    gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    //}

    private void Relampago()
    {
        StartCoroutine(nameof(ActivacionRe));
        
    }

    private IEnumerator Aturdir(Collider2D col)
    {
        if (col.CompareTag("EnemigoBasico"))
        {
            col.transform.GetComponent<MovimientoEnemigo>().CambiarAturdido(true);
            yield return new WaitForSeconds(duracionAturdimientoRE);
            col.transform.GetComponent<MovimientoEnemigo>().CambiarAturdido(false);
        }

        if (col.CompareTag("Aventurero"))
        {
            col.transform.GetComponent<Aventurero>().CambiarAturdido(true);
            yield return new WaitForSeconds(duracionAturdimientoRE);
            col.transform.GetComponent<Aventurero>().CambiarAturdido(false);
        }
    }

    private IEnumerator RepresentarRelampago()
    {
        representacionPreparacionRE.gameObject.SetActive(false);
        representacionAtaqueRE.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        representacionAtaqueRE.gameObject.SetActive(false);
    }

    private IEnumerator ActivacionRe()
    {
        representacionPreparacionRE.gameObject.SetActive(true);
        yield return new WaitForSeconds(tiempoCargaRE);
        GolpeRe();
        StartCoroutine(nameof(RepresentarRelampago));
    }

    private void GolpeRe()
    {
        Collider2D[] areaRelampago = Physics2D.OverlapBoxAll(posicionControladorGolpeRE.position, areaGolpeRE, 0);
        foreach (Collider2D col in areaRelampago)
        {
            if (col.CompareTag("EnemigoBasico"))
            {
                col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-dagnoGolpeRE);
                StartCoroutine(nameof(Aturdir));
                Debug.Log("Enemigo Herido");
            }

            if (col.CompareTag("Jefe"))
            {
                col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-dagnoGolpeRE);
                Debug.Log("Enemigo Herido");
            }

            if (col.CompareTag("EnemigoMina"))
            {
                col.transform.GetComponent<AtaqueMina>().ModificarVidaEnemigo(-dagnoGolpeRE);
                Debug.Log("Enemigo Herido");
            }

            if (col.CompareTag("Aventurero"))
            {
                col.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoGolpeRE);
                StartCoroutine(nameof(Aturdir));
                Debug.Log("Enemigo Herido");
            }

            //if (col.CompareTag("Pegote"))
            //{
            //    col.transform.GetComponent<FuncionamientoPegote>().ModificarVidaPegote(-dagnoGolpeRE);
            //    Debug.Log("Enemigo Herido");
            //}
        }
    }

    private void CorteAplastante()
    {
        GolpeCA();
        StartCoroutine(nameof(RepresentarAtaqueCA));
    }

    private IEnumerator RepresentarAtaqueCA()
    {
        representacionAtaqueCA.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        representacionAtaqueCA.gameObject.SetActive(false);
    }

    private void GolpeCA()
    {
        Collider2D[] areaGolpe = Physics2D.OverlapBoxAll(posicionControladorGolpeCA.position, areaGolpeCA, 0);
        foreach (Collider2D col in areaGolpe)
        {
            if (col.CompareTag("EnemigoBasico"))
            {
                col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-dagnoGolpeCA);
                col.transform.GetComponent<Rigidbody2D>().AddForce(fuerzaGolpeCA, ForceMode2D.Impulse);
                Debug.Log("Enemigo Herido");
            }

            if (col.CompareTag("Jefe"))
            {
                col.transform.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(-dagnoGolpeCA);
                col.transform.GetComponent<Rigidbody2D>().AddForce(fuerzaGolpeCA, ForceMode2D.Impulse);
                Debug.Log("Enemigo Herido");
            }

            if (col.CompareTag("EnemigoMina"))
            {
                col.transform.GetComponent<AtaqueMina>().ModificarVidaEnemigo(-dagnoGolpeCA);
                Debug.Log("Enemigo Herido");
            }

            if (col.CompareTag("Aventurero"))
            {
                col.transform.GetComponent<Aventurero>().ModificarVidaEnemigo(-dagnoGolpeCA);
                col.transform.GetComponent<Rigidbody2D>().AddForce(fuerzaGolpeCA, ForceMode2D.Impulse);
                Debug.Log("Enemigo Herido");
            }

            if (col.CompareTag("Pegote"))
            {
                col.transform.GetComponent<FuncionamientoPegote>().ModificarVidaPegote(-dagnoGolpeCA);
                Debug.Log("Enemigo Herido");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && esperaSiguienteAtaqueQ <= 0) {
            UsarQ();
        }
        if (Input.GetKeyDown(KeyCode.W) && esperaSiguienteAtaqueW <= 0 && munW > 0) {
            UsarW();
        }
        if (Input.GetKeyDown(KeyCode.E) && esperaSiguienteAtaqueE <= 0 && munE > 0) {
            UsarE();
        }
        if (Input.GetKeyDown(KeyCode.R) && esperaSiguienteAtaqueR <= 0 && munR > 0)
        {
            UsarR();
        }
        if (Input.GetKeyDown(KeyCode.T) && habilidadAleatoria != 0) {
            UsarT();
        }
        if (esperaSiguienteAtaqueQ > 0)
        {
            esperaSiguienteAtaqueQ -= Time.deltaTime;
        }
        if (esperaSiguienteAtaqueW > 0)
        {
            esperaSiguienteAtaqueW -= Time.deltaTime;
        }
        if (esperaSiguienteAtaqueE > 0)
        {
            esperaSiguienteAtaqueE -= Time.deltaTime;
        }
        if (esperaSiguienteAtaqueR > 0)
        {
            esperaSiguienteAtaqueR -= Time.deltaTime;
        }
    }

}
