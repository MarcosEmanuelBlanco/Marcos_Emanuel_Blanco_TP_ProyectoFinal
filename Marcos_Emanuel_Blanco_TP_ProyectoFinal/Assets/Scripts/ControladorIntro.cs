using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorIntro : MonoBehaviour
{
    private void Update()
    {
        CargarSiguienteEscena();
    }
    public void CargarSiguienteEscena()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(indiceEscenaActual + 1);
        }
    }
}
