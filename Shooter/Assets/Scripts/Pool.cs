using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    List<GameObject> pool;
    int poolSize;
    GameObject prefab;
    public Vector3 position;
    Transform parent;

    public Pool(int amount, GameObject pf, Vector3 pos, Transform par)
    {
        pool = new List<GameObject>();
        poolSize = amount;
        prefab = pf;
        position = pos;
        parent = par;

        for(int i=0;i<amount;i++)
        {
            GameObject g = GameObject.Instantiate(pf, pos, pf.transform.rotation, par);
            g.SetActive(false);
            pool.Add(g);
        }
    }

    public GameObject GetActor()
    {
        GameObject toReturn = new GameObject();

        if (pool.Count > 0)
        {
            if(!SearchForActiveActor(ref toReturn))
            {
                toReturn = GameObject.Instantiate(prefab, position, prefab.transform.rotation, parent);
                pool.Add(toReturn);
            }
        }
        else
        {
            toReturn = GameObject.Instantiate(prefab, position, prefab.transform.rotation, parent);
            pool.Add(toReturn);
        }
        return toReturn;
    }

    bool SearchForActiveActor(ref GameObject searched)
    {
        foreach (GameObject g in pool)
        {
            if (!g.activeSelf)
            {
                g.SetActive(true);
                GameObject.Destroy(searched);
                searched = null;
                searched = g;
                return true;
            }
        }

        return false;
    }

    public void ReturnActorToPool(GameObject actor)
    {
        foreach(GameObject g in pool)
        {
            if(g == actor)
            {
                actor.SetActive(false);
                actor.transform.position = position;
                actor.transform.rotation = prefab.transform.rotation;
            }
        }
    }
}
