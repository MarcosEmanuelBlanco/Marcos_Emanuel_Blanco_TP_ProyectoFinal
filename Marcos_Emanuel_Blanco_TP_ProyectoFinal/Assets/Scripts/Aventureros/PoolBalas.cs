using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBalas : MonoBehaviour
{
    [SerializeField] private GameObject bala;
    [SerializeField] private int tamanoPoolBala = 3;
    private List<GameObject> poolB;
    // Start is called before the first frame update
    void Start()
    {
        poolB = new List<GameObject>();
        for (int i = 0; i < tamanoPoolBala; i++)
        {
            GameObject obj = Instantiate(bala);
            obj.SetActive(false);
            poolB.Add(obj);
        }
    }

    public GameObject GetPooledBalas()
    {
        foreach (GameObject obj in poolB)
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
