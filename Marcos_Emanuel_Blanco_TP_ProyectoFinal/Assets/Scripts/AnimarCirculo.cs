using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimarCirculo : MonoBehaviour
{
    private Animator animatorCirculo;
    void Start()
    {
        animatorCirculo = GetComponent<Animator>();
    }

    public void AnimacionEnfriamiento()
    {
        animatorCirculo.SetTrigger("Enfriamiento");
    }
}
