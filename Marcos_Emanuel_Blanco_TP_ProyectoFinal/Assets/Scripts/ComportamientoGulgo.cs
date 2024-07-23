using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoGulgo : MonoBehaviour
{
    [Header("Corre y muerde")]

    [SerializeField] private Transform representacionAtaqueMordisco;
    [SerializeField] Transform puntoMordisco;
    [SerializeField] Vector2 alcanceMordisco;
    [SerializeField] float dagnoMordisco;
    [SerializeField] float dagnoVeneno;
    [SerializeField] float duracionVeneno;
    [SerializeField] float lapsoTicksVeneno;
    [SerializeField] float tiempoEntreCorreteos;
    [SerializeField] float duracionDeCorreteo;
    [SerializeField] float distanciaCorreteo;
    private float velocidadCorreteoReal;
    [SerializeField] float divisorDuracionDeCorreteo;
    private float tiempo;

    [Header("Raíces")]

    [SerializeField] private Transform representacionAtaqueRaices;
    [SerializeField] private Vector2 alcanceRaices;
    [SerializeField] Transform puntoRaices;
    [SerializeField] private int dagnoRaices;
    [SerializeField] private float tiempoCargaRaices;
    [SerializeField] private float tiempoEntreRaices;

    [Header("Cuerpo a cuerpo")]

    [SerializeField] private Transform representacionAtaqueCuerpoACuerpo;
    [SerializeField] private Vector2 alcanceCuerpoACuerpo;
    [SerializeField] Transform puntoCuerpoACuerpo;
    [SerializeField] private int dagnoCuerpoACuerpo;
    [SerializeField] private float intervaloEntreGolpes;
    [SerializeField] private int cantidadGolpes;
    [SerializeField] private float tiempoEntreGolpizas;

    [Header("Escupitajo")]

    [SerializeField] private GameObject proyectilGulgo;
    [SerializeField] private Transform puntoDisparoGulgo;
    [SerializeField] private float tiempoCargaDisparo;
    [SerializeField] private float tiempoEntreDisparos;

    [Header("Esporas")]

    [SerializeField] private Transform representacionAtaqueEsporas;
    [SerializeField] private Vector2 alcanceEsporas;
    [SerializeField] Transform puntoEsporas;
    [SerializeField] private int dagnoEsporas;
    [SerializeField] private float tiempoCargaEsporas;
    [SerializeField] private float tiempoEntreEsporas;

    //[SerializeField] float tiempoEntreDisparos;
    //[SerializeField] float tiempoEntreSaltos;
    //[SerializeField] private float fuerzaSalto = 5f;

    //[SerializeField] float tiempoDeCarga; //Variable que se usa para demorar el disparo y ejecutar una animación (no implementada aún).


    //[SerializeField] GameObject[] orbesRojas;
    //[SerializeField] Transform[] puntoDisparoBola;

    //[SerializeField] private float fuerzaEstruendo;

    //private Rigidbody2D miRigidbody2D;
    //private SpriteRenderer miSprite;
    //private Animator miAnimator;
    //private BoxCollider2D miBoxCollider;
    //private int saltoMask;

    private float tiempoActualEspera;
    private int estadoActual;

    private const int Disparo = 0;
    private const int Esporas = 1;
    private const int Correteo = 2;
    private const int EcharRaices = 3;
    private const int Golpiza = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        velocidadCorreteoReal = distanciaCorreteo;
        estadoActual = Correteo;
        StartCoroutine(Espera());
        
        representacionAtaqueCuerpoACuerpo.gameObject.SetActive(false);
        representacionAtaqueMordisco.gameObject.SetActive(false);
        representacionAtaqueRaices.gameObject.SetActive(false);
        representacionAtaqueEsporas.gameObject.SetActive(false);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Suelo"))
    //    {
    //        Collider2D[] objetos = Physics2D.OverlapBoxAll(areaEstruendo.position, tamanoEstruendo, 0);
    //        foreach (Collider2D col in objetos)
    //        {
    //            if (col.CompareTag("Player"))
    //            {
    //                col.transform.GetComponent<Movimiento>().GetBody().AddForce(Vector2.up * fuerzaEstruendo, ForceMode2D.Impulse);
    //                col.transform.GetComponent<Jugador>().ModificarVida(-dagnoEstruendo);
    //            }
    //        }
    //    }

    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.GetComponent<Movimiento>().Rebotar(collision.GetContact(0).normal);
    //    }
    //}

    private IEnumerator Espera()
    {
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(Comportamientos());
    }

    private void Update()
    {
        //miAnimator.SetBool("JefeEnAire", !JefeEnContactoConPlataforma());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(puntoMordisco.position, alcanceMordisco);
        Gizmos.DrawWireCube(puntoRaices.position, alcanceRaices);
        Gizmos.DrawWireCube(puntoCuerpoACuerpo.position, alcanceCuerpoACuerpo);
        Gizmos.DrawWireCube(puntoEsporas.position, alcanceEsporas);
    }
    // Codigo ejecutado cuando el objeto se activa en el nivel
    private void OnEnable()
    {
        //miRigidbody2D = GetComponent<Rigidbody2D>();
        //miSprite = GetComponent<SpriteRenderer>();
        //miAnimator = GetComponent<Animator>();
        //miBoxCollider = GetComponent<BoxCollider2D>();
        //saltoMask = LayerMask.GetMask("Plataformas");
    }
    private IEnumerator Comportamientos()
    {
        while (true)
        {
            switch (estadoActual)
            {
                case Disparo:
                    //miAnimator.SetTrigger("Cargando");
                    StartCoroutine(Disparar());
                    tiempoActualEspera = tiempoEntreDisparos;
                    break;
                case Esporas:
                    StartCoroutine(ActivarEsporas());
                    tiempoActualEspera = tiempoEntreEsporas;
                    break;
                case Correteo:
                    StartCoroutine(Mordisco());
                    tiempoActualEspera = tiempoEntreCorreteos;
                    break;
                case EcharRaices:
                    StartCoroutine(Raices());
                    tiempoActualEspera = tiempoEntreRaices;
                    break;
                case Golpiza:
                    ActivarGolpiza();
                    tiempoActualEspera = tiempoEntreGolpizas;
                    break;
            }
            Debug.Log(estadoActual);
            yield return new WaitForSeconds(tiempoActualEspera);
            ActualizarEstado();
        }
    }

    private void ActualizarEstado()
    {
        // Actualiza el estado actual según las probabilidades y condiciones que desees
        // Puedes usar Random.Range para generar números aleatorios y decidir el siguiente estado
        estadoActual = Random.Range(0, 5);
    }

    private IEnumerator Mordisco()
    {
        //miAnimator.SetBool("Correteando", true);
        tiempo = Time.time;
        Vector2 posicionInicial = transform.position;
        Vector2 posicionObjetivo = new(transform.position.x - distanciaCorreteo, transform.position.y); //Debería ser la del jugador.
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        GameObject muro = GameObject.FindGameObjectWithTag("Muro");
        //if(muro != null)
        //{
        //    posicionObjetivo = new(transform.position.x - muro.transform.position.x, transform.position.y);
        //}
        //else
        //{
        //    posicionObjetivo = new(transform.position.x - jugador.transform.position.x, transform.position.y);
        //}
        while (Time.time < tiempo + duracionDeCorreteo / divisorDuracionDeCorreteo)
        {
            transform.position = Vector2.Lerp(posicionInicial, posicionObjetivo, (Time.time - tiempo) / (duracionDeCorreteo / divisorDuracionDeCorreteo));
            yield return null;
        }
        GolpeMordisco();
        //miSprite.flipX = !miSprite.flipX;
        // Mover hacia atrás (retroceso)
        tiempo = Time.time;
        while (Time.time < tiempo + duracionDeCorreteo / divisorDuracionDeCorreteo)
        {
            transform.position = Vector2.Lerp(posicionObjetivo, posicionInicial, (Time.time - tiempo) / (duracionDeCorreteo / divisorDuracionDeCorreteo));

            yield return null;
        }
        //miAnimator.SetBool("Correteando", false);
        //miSprite.flipX = !miSprite.flipX;
    }

    void GolpeMordisco()
    {
        Collider2D[] areaMordisco = Physics2D.OverlapBoxAll(puntoMordisco.position, alcanceMordisco, 0);
        foreach (Collider2D col in areaMordisco)
        {
            if (col.CompareTag("Player"))
            {
                StartCoroutine(nameof(RepresentarMordisco));
                col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoMordisco);
                gameObject.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(dagnoMordisco * 0.5f);
                tiempo = Time.time;
                StartCoroutine(nameof(ActivacionVeneno));
            }
        }
    }

    private IEnumerator RepresentarMordisco()
    {
        representacionAtaqueMordisco.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        representacionAtaqueMordisco.gameObject.SetActive(false);
    }

    private IEnumerator ActivacionVeneno()
    {
        InvokeRepeating(nameof(DagnarConVeneno), 0, lapsoTicksVeneno);
        yield return new WaitForSeconds(duracionVeneno);
        CancelInvoke(nameof(DagnarConVeneno));
    }

    void DagnarConVeneno()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        jugador.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoVeneno);
    }

    public void ModificarVelocidadCorreteo(float puntos)
    {
        distanciaCorreteo += puntos;
    }

    public void RestaurarVelocidadCorreteo()
    {
        distanciaCorreteo = velocidadCorreteoReal;
    }

    private IEnumerator Raices()
    {
        yield return new WaitForSeconds(tiempoCargaRaices);
        GolpeRaices();
    }

    private IEnumerator RepresentarRaices()
    {
        representacionAtaqueRaices.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        representacionAtaqueRaices.gameObject.SetActive(false);
    }

    private void GolpeRaices()
    {
        Collider2D[] areaRaices = Physics2D.OverlapBoxAll(puntoRaices.position, alcanceRaices, 0);
        foreach (Collider2D col in areaRaices)
        {
            if (col.CompareTag("Player"))
            {
                StartCoroutine(nameof(RepresentarRaices));
                col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoRaices);
                gameObject.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(dagnoRaices * 0.5f);
            }

            if (col.CompareTag("Invocacion"))
            {
                StartCoroutine(nameof(RepresentarRaices));
                col.transform.GetComponent<Invocacion>().ModificarVidaEnemigo(-dagnoRaices);
                gameObject.GetComponent<EnemigoPrevisional>().ModificarVidaEnemigo(dagnoRaices * 0.5f);
                Debug.Log("Enemigo Herido");
            }
        }
    }

    private IEnumerator CorrerParaGolpear()
    {
        //miAnimator.SetBool("Correteando", true);
        tiempo = Time.time;
        Vector2 posicionInicial = transform.position;
        Vector2 posicionObjetivo = new(transform.position.x - distanciaCorreteo, transform.position.y); //Debería ser la del jugador.

        while (Time.time < tiempo + duracionDeCorreteo / divisorDuracionDeCorreteo)
        {
            transform.position = Vector2.Lerp(posicionInicial, posicionObjetivo, (Time.time - tiempo) / (duracionDeCorreteo / divisorDuracionDeCorreteo));
            yield return null;
        }
        ActivarGolpiza();
        //miSprite.flipX = !miSprite.flipX;
        // Mover hacia atrás (retroceso)
        tiempo = Time.time;
        while (Time.time < tiempo + duracionDeCorreteo / divisorDuracionDeCorreteo)
        {
            transform.position = Vector2.Lerp(posicionObjetivo, posicionInicial, (Time.time - tiempo) / (duracionDeCorreteo / divisorDuracionDeCorreteo));

            yield return null;
        }
        //miAnimator.SetBool("Correteando", false);
        //miSprite.flipX = !miSprite.flipX;
    }

    private void ActivarGolpiza()
    {
        StartCoroutine(nameof(GolpeCAC));
    }

    private IEnumerator GolpeCAC()
    {
        for(int i = 0; i < cantidadGolpes;i++)
        {
            yield return new WaitForSeconds(intervaloEntreGolpes);
            Collider2D[] areaRaices = Physics2D.OverlapBoxAll(puntoCuerpoACuerpo.position, alcanceCuerpoACuerpo, 0);
            foreach (Collider2D col in areaRaices)
            {
                if (col.CompareTag("Player"))
                {
                    StartCoroutine(nameof(RepresentarGolpiza));
                    col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoCuerpoACuerpo);
                }

                if (col.CompareTag("Pegote"))
                {
                    StartCoroutine(nameof(RepresentarGolpiza));
                    col.transform.GetComponent<FuncionamientoPegote>().ModificarVidaPegote(-dagnoCuerpoACuerpo);
                }
            }
        }
    }

    private IEnumerator RepresentarGolpiza()
    {
        representacionAtaqueCuerpoACuerpo.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        representacionAtaqueCuerpoACuerpo.gameObject.SetActive(false);
    }

    private IEnumerator Disparar()
    {
        yield return new WaitForSeconds(tiempoCargaDisparo);
        Instantiate(proyectilGulgo, puntoDisparoGulgo.position, Quaternion.identity);
    }

    private IEnumerator ActivarEsporas()
    {
        yield return new WaitForSeconds(tiempoCargaRaices);
        GolpeEsporas();
    }

    private IEnumerator RepresentarEsporas()
    {
        representacionAtaqueEsporas.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        representacionAtaqueEsporas.gameObject.SetActive(false);
    }

    private void GolpeEsporas()
    {
        Collider2D[] areaEsporas = Physics2D.OverlapBoxAll(puntoEsporas.position, alcanceEsporas, 0);
        foreach (Collider2D col in areaEsporas)
        {
            if (col.CompareTag("Player"))
            {
                StartCoroutine(nameof(RepresentarEsporas));
                col.transform.GetComponent<EstadoJugador>().ModificarVidaJugador(-dagnoEsporas);
            }

            if (col.CompareTag("Invocacion"))
            {
                StartCoroutine(nameof(RepresentarEsporas));
                col.transform.GetComponent<Invocacion>().ModificarVidaEnemigo(-dagnoEsporas);
                Debug.Log("Enemigo Herido");
            }
        }
    }
    //private bool JefeEnContactoConPlataforma()
    //{
    //    return miBoxCollider.IsTouchingLayers(saltoMask);
    //}
    //private IEnumerator Disparar()
    //{
    //    yield return new WaitForSeconds(tiempoDeCarga);

    //    for (int i = 0; i < 5; i++)
    //    {
    //        Instantiate(orbesRojas[i], puntoDisparoBola[i].position, Quaternion.identity);
    //    }
    //}

    //private IEnumerator Saltar()
    //{
    //    miRigidbody2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse); //Más adelante se pondrá aquí una función para que cree un área de daño al tocar el piso.
    //    yield return null;
    //}


}
