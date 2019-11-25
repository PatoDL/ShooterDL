using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public delegate void OnItemCollected(GameObject g);

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Player")
        {

        }
    }
}
