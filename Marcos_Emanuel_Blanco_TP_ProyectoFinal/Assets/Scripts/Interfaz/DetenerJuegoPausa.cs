using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DetenerJuegoPausa : MonoBehaviour
{
    private void DetenerTiempoPausa()
    {
        Debug.Log("Pausa");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
