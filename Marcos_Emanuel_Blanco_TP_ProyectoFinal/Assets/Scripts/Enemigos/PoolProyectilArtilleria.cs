using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolProyectilArtilleria : MonoBehaviour
{
    [SerializeField] private GameObject bomba;
    [SerializeField] private int tamanoPoolBomba = 3;
    private List<GameObject> poolBomba;
    // Start is called before the first frame update
    void Start()
    {
        poolBomba = new List<GameObject>();
        for (int i = 0; i < tamanoPoolBomba; i++)
        {
            GameObject obj = Instantiate(bomba);
            obj.SetActive(false);
            poolBomba.Add(obj);
        }
    }

    public GameObject GetPooledBomba()
    {
        foreach (GameObject obj in poolBomba)
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
