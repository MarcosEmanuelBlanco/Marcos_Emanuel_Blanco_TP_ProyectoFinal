using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolProyectilGulgo : MonoBehaviour
{
    [SerializeField] private GameObject bolaPegote;
    [SerializeField] private int tamanoPoolBolaPegote = 1;
    private List<GameObject> poolPe;
    // Start is called before the first frame update
    void Start()
    {
        poolPe = new List<GameObject>();
        for (int i = 0; i < tamanoPoolBolaPegote; i++)
        {
            GameObject obj = Instantiate(bolaPegote);
            obj.SetActive(false);
            poolPe.Add(obj);
        }
    }

    public GameObject GetPooledBolasPegote()
    {
        foreach (GameObject obj in poolPe)
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
