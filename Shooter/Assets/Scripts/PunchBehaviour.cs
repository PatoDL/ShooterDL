using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBehaviour : MonoBehaviour
{
    public float speed;
    public bool launched;
    public delegate void ReturnToPool(GameObject punch);
    public static ReturnToPool ReturnGO;

    // Start is called before the first frame update
    void Start()
    {
        launched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(launched)
            transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void Launch()
    {
        launched = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Punch" && launched)
        {
            launched = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            ReturnGO(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.tag == "Finish")
        {
            launched = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            ReturnGO(this.gameObject);
        }
    }
}
