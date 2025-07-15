using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolProyectilW : MonoBehaviour
{
    [SerializeField] private GameObject bolaInfernal;
    [SerializeField] private int tamanoPoolBolaInfernal = 3;
    private List<GameObject> poolBIn;
    // Start is called before the first frame update
    void Start()
    {
        poolBIn = new List<GameObject>();
        for (int i = 0; i < tamanoPoolBolaInfernal; i++)
        {
            GameObject obj = Instantiate(bolaInfernal);
            obj.SetActive(false);
            poolBIn.Add(obj);
        }
    }

    public GameObject GetPooledBolasInfernales()
    {
        foreach (GameObject obj in poolBIn)
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
