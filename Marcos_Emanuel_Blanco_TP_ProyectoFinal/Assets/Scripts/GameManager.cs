using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool puedeVolver;
    private bool juegoDetenido;
    private bool gulgoVivo;
    [SerializeField] private TextMeshProUGUI textoJuegoDetenido;
    [SerializeField] private UnityEvent<string> OnRemainingSoulsChange;
    // Start is called before the first frame update
    void Start()
    {
        juegoDetenido = false;
        puedeVolver = true;
        gulgoVivo = true;
        textoJuegoDetenido.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Derribado();
        RevivirOReiniciar();
        AreaAsegurada();
        if(Input.GetKeyDown(KeyCode.Space) && puedeVolver && juegoDetenido)
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");
            jugador.GetComponent<EstadoJugador>().Revivir();
        }

        if(Input.GetKeyDown(KeyCode.Space) && !puedeVolver && juegoDetenido)
        {
            ReiniciarEscena();
        }

        if(Input.GetKeyDown(KeyCode.Space) && !gulgoVivo && juegoDetenido)
        {
            ReiniciarEscena();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && juegoDetenido)
        {
            VolverAlMenu();
        }
    }

    void Derribado()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador.GetComponent<EstadoJugador>().GetVidaActual() <= 0)
        {
            textoJuegoDetenido.gameObject.SetActive(true);
            Time.timeScale = 0;
            juegoDetenido = true;
        }
        else
        {
            juegoDetenido = false;
            Time.timeScale = 1;
            textoJuegoDetenido.gameObject.SetActive(false);
        }
    }

    void ReiniciarEscena()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        juegoDetenido = false;
    }

    void VolverAlMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        juegoDetenido = false;
    }

    void RevivirOReiniciar()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador.GetComponent<EstadoJugador>().GetAlmas() > 0)
        {
            OnRemainingSoulsChange.Invoke("Te derrotaron, pero no es el fin. Consume 1 alma con ESPACIO para continuar.");
            puedeVolver = true;
        }
        else
        {
            OnRemainingSoulsChange.Invoke("Te quedaste sin almas. Presiona ESPACIO para reiniciar.");
            puedeVolver = false;
        }
    }

    void AreaAsegurada()
    {
        GameObject gulgo = GameObject.FindGameObjectWithTag("Jefe");
        if(gulgo != null && gulgo.GetComponent<EnemigoPrevisional>().GetVidaActual() <= 0)
        {
            gulgoVivo = false;
            OnRemainingSoulsChange.Invoke("ÁREA ASEGURADA. Presioná ESPACIO para reiniciar o ESCAPE para volver.");
            textoJuegoDetenido.gameObject.SetActive(true);
            Time.timeScale = 0;
            juegoDetenido = true;
        }
    }
}
