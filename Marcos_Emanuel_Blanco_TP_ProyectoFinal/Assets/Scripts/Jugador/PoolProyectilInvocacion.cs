using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolProyectilInvocacion : MonoBehaviour
{
    [SerializeField] private GameObject bolaFuego;
    [SerializeField] private int tamanoPoolBolaFuego = 3;
    private List<GameObject> poolBF;
    // Start is called before the first frame update
    void Start()
    {
        poolBF = new List<GameObject>();
        for (int i = 0; i < tamanoPoolBolaFuego; i++)
        {
            GameObject obj = Instantiate(bolaFuego);
            obj.SetActive(false);
            poolBF.Add(obj);
        }
    }

    public GameObject GetPooledBolasFuego()
    {
        foreach (GameObject obj in poolBF)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
