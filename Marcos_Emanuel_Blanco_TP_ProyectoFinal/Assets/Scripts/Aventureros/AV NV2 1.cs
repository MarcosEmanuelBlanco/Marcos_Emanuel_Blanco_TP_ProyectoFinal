using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVNV21 : MonoBehaviour
{
    private void ActivarCuadro1()
    {
        if (gameObject.GetComponent<Aventurero>().GetVidaAv() <= 0)
        {
            GameObject cartel1 = GameObject.FindGameObjectWithTag("Cartel1");
            if (!cartel1.activeInHierarchy)
            {
                cartel1.SetActive(true);
            }
        }
    }

    private void Update()
    {
        ActivarCuadro1();
    }
}
