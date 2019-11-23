using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject player;
    Vector3 direction;
    public delegate void OnReturn(GameObject returnableEnemy);
    public static OnReturn ReturnEnemy;
    Rigidbody rig;
    public bool nearPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        direction = player.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        nearPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!nearPlayer && direction != player.transform.position - transform.position)
        {
            direction = player.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        }
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "Player" || collision.transform.tag == "Punch")
        {
            ReturnEnemy(gameObject);
            nearPlayer = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GameController")
            nearPlayer = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GameController" && !nearPlayer)
        {
            nearPlayer = true;
        }
    }
}
