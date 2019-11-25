using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    Pool items;

    public GameObject item;
    public int amount;

    public GameObject itemSpawns;

    GameObject[] itemSpawn;

    // Start is called before the first frame update
    void Start()
    {
        itemSpawn = new GameObject[itemSpawns.transform.childCount];
        items = new Pool(amount, item, transform.position, transform);

        for(int i=0;i<itemSpawns.transform.childCount;i++)
        {
            itemSpawn[i] = itemSpawns.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
