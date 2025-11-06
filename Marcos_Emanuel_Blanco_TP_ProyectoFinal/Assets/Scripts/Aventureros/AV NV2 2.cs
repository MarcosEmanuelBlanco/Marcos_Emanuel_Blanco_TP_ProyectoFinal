using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVNV22 : MonoBehaviour
{
    private void ActivarCuadro2()
    {
        if (gameObject.GetComponent<Aventurero>().GetVidaAv() <= 0)
        {
            GameObject cartel2 = GameObject.FindGameObjectWithTag("Cartel2");
            if (!cartel2.activeInHierarchy)
            {
                cartel2.SetActive(true);
            }
        }
    }

    private void Update()
    {
        ActivarCuadro2();
    }
}
