using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int amount;
    Pool punches;
    PunchBehaviour nextLauched;
    public GameObject punchParent;
    bool canLaunch;
    // Start is called before the first frame update
    void Start()
    {
        punches = new Pool(amount, prefab, transform.position, transform);
        PunchBehaviour.ReturnGO = ReturnToPool;
        nextLauched = punches.GetActor().GetComponent<PunchBehaviour>();
        canLaunch = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canLaunch)
        {
            nextLauched.Launch();
            //punches.position = nextLauched.transform.position;
            canLaunch = false;
            Invoke("UpdateNextLaunched", 0.5f);
        }
    }

    void ReturnToPool(GameObject go)
    {
        punches.ReturnActorToPool(go);
    }

    void UpdateNextLaunched()
    {
        canLaunch = true;
        nextLauched = punches.GetActor().GetComponent<PunchBehaviour>();
    }
}
