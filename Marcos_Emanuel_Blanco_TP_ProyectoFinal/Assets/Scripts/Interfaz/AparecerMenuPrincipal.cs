using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerMenuPrincipal : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    private void AparicionMenu()
    {
        menu.SetActive(true);
    }
}
