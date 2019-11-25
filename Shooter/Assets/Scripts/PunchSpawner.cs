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

    public float launchTime;
    public float launchTimeMax;

    // Start is called before the first frame update
    void Start()
    {
        punches = new Pool(amount, prefab, transform.position, punchParent.transform);
        PunchBehaviour.ReturnGO = ReturnToPool;
        nextLauched = punches.GetActor().GetComponent<PunchBehaviour>();
        canLaunch = true;
        launchTime = launchTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (canLaunch)
        {
            nextLauched.transform.position = transform.position;
            nextLauched.transform.rotation = transform.rotation;

#if UNITY_STANDALONE || UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                nextLauched.Launch();
                //punches.position = nextLauched.transform.position;
                canLaunch = false;
                Invoke("UpdateNextLaunched", 0.5f);
            }
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            launchTime -= Time.deltaTime;
            if (launchTime <= 0f)
            {
                nextLauched.Launch();
                canLaunch = false;
                Invoke("UpdateNextLaunched", 0.5f);
                launchTime = launchTimeMax;
            }
#endif
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

    public void ResetPunches()
    {
        punches.ResetPool();
    }
}
