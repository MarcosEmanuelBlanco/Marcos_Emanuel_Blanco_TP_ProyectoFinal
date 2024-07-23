using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverLimite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mover();        
    }

    void Mover()
    {
        GameObject gulgo = GameObject.FindGameObjectWithTag("Jefe");
        if (gulgo != null)
        {
            transform.position = new(490.0f,0.0f,0.0f);
        }
    }
}
