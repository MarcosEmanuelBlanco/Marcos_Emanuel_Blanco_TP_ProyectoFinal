using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoRaices : MonoBehaviour
{
    [SerializeField] private AudioClip efecto;
    private AudioSource sonidoEfecto;
    private void Start()
    {
        sonidoEfecto = GetComponent<AudioSource>();
    }

    private void playSonido()
    {
        sonidoEfecto.PlayOneShot(efecto);
    }
}
