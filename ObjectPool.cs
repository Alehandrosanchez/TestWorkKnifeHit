using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] private List<GameObject> pooled_Objects = new List<GameObject>();
    [SerializeField] private int amount_ToPool = 20;


    [SerializeField] private GameObject Knife_Prefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < amount_ToPool; i++)
        {
            GameObject obj = Instantiate(Knife_Prefab);
            obj.SetActive(false);
            pooled_Objects.Add(obj);
            obj.transform.parent = this.transform;
        }
    }
    void Start()
    {
        
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooled_Objects.Count; i++)
        {
            if (!pooled_Objects[i].activeInHierarchy)
            {
                return pooled_Objects[i];
            }
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
