using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AtaquesPrevisional : MonoBehaviour
{
    private Animator animatorCuerpo;
    private AudioSource sonidosDhaork;
    [SerializeField] private bool habilidadActiva;

    [Header("Q")]

    [SerializeField] private Transform posicionControladorGolpeQ;
    [SerializeField] private GameObject brazoDerecho;
    [SerializeField] private float esperaSiguienteAtaqueQ;
    [SerializeField] private float intervaloEntreGolpesQ;
    [SerializeField] private float radioGolpeQ;
    [SerializeField] private float dagnoGolpeQ;

    [Header("W")]

    [SerializeField] private GameObject proyectilW;
    [SerializeField] private Transform puntoDisparoW;
    [SerializeField] private float esperaSiguienteAtaqueW;
    [SerializeField] private float intervaloEntreGolpesW;
    [SerializeField] private PoolProyectilW poolBolasInfernales;
    [SerializeField] private GameObject circuloW;

    [Header("E")]

    [SerializeField] private GameObject portales;
    [SerializeField] private GameObject[] grupoInvocacionesE;
    [SerializeField] private Transform[] puntoInvocacionE;
    [SerializeField] private float esperaSiguienteAtaqueE;
    [SerializeField] private float intervaloEntreGolpesE;
    [SerializeField] private GameObject circuloE;

    [Header("R")]

    [SerializeField] private GameObject muroR;
    [SerializeField] private Transform puntoMuro;
    [SerializeField] private Transform explosionMuro;
    [SerializeField] private float esperaSiguienteAtaqueR;
    [SerializeField] private float intervaloEntreGolpesR;
    [SerializeField] private GameObject circuloR;

    [Header("Aleatoria")]

    [SerializeField] private int habilidadAleatoria;
    //private bool aleatoriaDisponible;

    [Header("Corte Aplastante")]

    [SerializeField] private GameObject representacionAtaqueCA;
    [SerializeField] private Transform posicionControladorGolpeCA;
    [SerializeField] private Vector2 areaGolpeCA;
    [SerializeField] private float dagnoGolpeCA;
    [SerializeField] private Vector2 fuerzaGolpeCA;

    [Header("Relámpago")]

    [SerializeField] private GameObject representacionAtaqueRE;
    [SerializeField] private Transform posicionControladorGolpeRE;
    [SerializeField] private Vector2 areaGolpeRE;
    [SerializeField] private float dagnoGolpeRE;
    [SerializeField] private float duracionAturdimientoRE;
    [SerializeField] private float tiempoCargaRE;

    [Header("Invocar Artillería")]

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

    [Header("Sonidos")]
    [SerializeField] private AudioClip lanzamientoBDF;
    [SerializeField] private AudioClip cargaRelampago;
    [SerializeField] private AudioClip efectoRecarga;
    [SerializeField] private AudioClip detonacionMuro;
    [SerializeField] private AudioClip chasquidoPortal;

    void Start()
    {
        animatorCuerpo = GetComponent<Animator>();
        sonidosDhaork = GetComponent<AudioSource>();
        habilidadActiva = false;
        //aleatoriaDisponible = false;
        explosionMuro.gameObject.SetActive(false);
        representacionAtaqueCA.gameObject.SetActive(false);
        representacionAtaqueRE.gameObject.SetActive(false);
        //representacionPreparacionRE.gameObject.SetActive(false);
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

    private void SonidoBDF()
    {
        sonidosDhaork.PlayOneShot(lanzamientoBDF);
    }

    private void SonidoRE()
    {
        sonidosDhaork.PlayOneShot(cargaRelampago);
    }

    private void SonidoRecarga()
    {
        sonidosDhaork.PlayOneShot(efectoRecarga);
    }

    private void SonidoMuro()
    {
        sonidosDhaork.PlayOneShot(detonacionMuro);
    }

    private void SonidoChasquido()
    {
        sonidosDhaork.PlayOneShot(chasquidoPortal);
    }

    private void Update()
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(posicionControladorGolpeQ.position, radioGolpeQ);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(posicionControladorGolpeCA.position, areaGolpeCA);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(posicionControladorGolpeRE.position, areaGolpeRE);
    }

    public bool DetectarHabilidadActiva()
    {
        return habilidadActiva;
    }

    public void TerminarHabilidad()
    {
        animatorCuerpo.SetBool("HabilidadActiva", false);
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
                OnRandomSkillChange.Invoke("Relámpago");
                break;
            case 3:
                OnRandomSkillChange.Invoke("Recarga");
                break;
            case 4:
                OnRandomSkillChange.Invoke("Invocar Artillería");
                break;
        }
    }

    private void ReactivarBrazo()
    {
        brazoDerecho.GetComponent<AnimarBrazo>().BrazoHabilidadInactiva();
    }

    public void ParalizarPorHabilidad()
    {
        gameObject.GetComponent<Movimiento>().CambiarAturdido(true);
    }

    public void ContinuarMovimiento()
    {
        gameObject.GetComponent<Movimiento>().CambiarAturdido(false);
    }
    private void UsarQ()
    {
        animatorCuerpo.SetBool("HabilidadActiva", true);
        esperaSiguienteAtaqueQ = intervaloEntreGolpesQ;
        animatorCuerpo.SetTrigger("GolpeBasico");
        brazoDerecho.GetComponent<AnimarBrazo>().AnimacionGolpeBasico();
        brazoDerecho.GetComponent<AnimarBrazo>().BrazoHabilidadActiva();
    }
  
    public void GolpeQ()
    {
        Collider2D[] areaGolpe = Physics2D.OverlapCircleAll(posicionControladorGolpeQ.position, radioGolpeQ);
        foreach (Collider2D col in areaGolpe)
        {
            if (col.CompareTag("EnemigoBasico") || col.CompareTag("Jefe"))
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
        animatorCuerpo.SetTrigger("BolaDeFuego");
        animatorCuerpo.SetBool("HabilidadActiva", true);//animatorCuerpo.SetBool("BrazoInerte", true);
        brazoDerecho.GetComponent<AnimarBrazo>().BrazoHabilidadActiva();
        circuloW.GetComponent<AnimarCirculo>().AnimacionEnfriamiento();
        munWActual--;
        OnWAmmoChange.Invoke(munWActual.ToString());
        esperaSiguienteAtaqueW = intervaloEntreGolpesW;
        
    }

    private void DisparoW()
    {
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
        animatorCuerpo.SetBool("HabilidadActiva", true);
        brazoDerecho.GetComponent<AnimarBrazo>().BrazoHabilidadActiva();
        circuloE.GetComponent<AnimarCirculo>().AnimacionEnfriamiento();
        munEActual--;
        OnEAmmoChange.Invoke(munEActual.ToString());
        esperaSiguienteAtaqueE = intervaloEntreGolpesE;
        animatorCuerpo.SetTrigger("InvocarSoldados");
        portales.SetActive(true);
    }

    private void DesactivarPortales()
    {
        portales.SetActive(false);
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
        animatorCuerpo.SetBool("HabilidadActiva", true);
        brazoDerecho.GetComponent<AnimarBrazo>().BrazoHabilidadActiva();
        circuloR.GetComponent<AnimarCirculo>().AnimacionEnfriamiento();
        munRActual--;
        OnRAmmoChange.Invoke(munRActual.ToString());
        esperaSiguienteAtaqueR = intervaloEntreGolpesR;
        animatorCuerpo.SetTrigger("ActivarEscudo");
    }

    private void InicioR()
    {
        muroR.SetActive(true);
    }

    private void ExplosionMuro()
    {
        muroR.GetComponent<FuncionamientoMuro>().DestruirMuro();
    }

    private void UsarT()
    {
        animatorCuerpo.SetBool("HabilidadActiva", true);
        switch (habilidadAleatoria)
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
    }

    private void CorteAplastante()
    {
        animatorCuerpo.SetBool("HabilidadActiva", true);
        animatorCuerpo.SetTrigger("CorteAplastante");
        brazoDerecho.GetComponent<AnimarBrazo>().AnimacionGolpeCA();
        brazoDerecho.GetComponent<AnimarBrazo>().BrazoHabilidadActiva();
    }

    public void InicioEfectoCA()
    {
        representacionAtaqueCA.SetActive(true);
    }

    public void GolpeCA()
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

    private void Relampago()
    {
        animatorCuerpo.SetBool("HabilidadActiva", true);
        brazoDerecho.GetComponent<AnimarBrazo>().BrazoHabilidadActiva();
        animatorCuerpo.SetTrigger("Relampago");
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

    private void RepresentarRelampago()
    {
        representacionAtaqueRE.SetActive(true);
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

    private void RecuperarMunicion()
    {
        animatorCuerpo.SetBool("HabilidadActiva", true);
        brazoDerecho.GetComponent<AnimarBrazo>().BrazoHabilidadActiva();
        animatorCuerpo.SetTrigger("Recargar");
        munWActual = munW;
        OnWAmmoChange.Invoke(munWActual.ToString());
        munEActual = munE;
        OnEAmmoChange.Invoke(munEActual.ToString());
        munRActual = munR;
        OnRAmmoChange.Invoke(munRActual.ToString());
        //StartCoroutine(nameof(ParpadeoMunicion));
    }

    private void InvocarArtilleria()
    {
        animatorCuerpo.SetBool("HabilidadActiva", true);
        brazoDerecho.GetComponent<AnimarBrazo>().BrazoHabilidadActiva();
        animatorCuerpo.SetTrigger("InvocarArtilleros");
        portales.SetActive(true);
    }

    private void AparicionArtilleria()
    {
        for (int i = 0; i < invocacionArtilleria.Length; i++)
        {
            GameObject nuevaArtilleria = invocacionArtilleria[i];
            nuevaArtilleria.transform.position = puntoArtilleria[i].transform.position;
            Instantiate(nuevaArtilleria);
        }
    }
}
