using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float speedH;
    public float speedV;

    float yaw;
    float pitch;

    public float speed;

    float yPos;

    Rigidbody rig;

    public int life;

    public bool gameOver;

    Vector3 startPos;

    public PunchSpawner ps;

    // Start is called before the first frame update
    void Start()
    {
        yPos = transform.parent.position.y;
        rig = GetComponentInParent<Rigidbody>();
#if UNITY_STANDALONE || UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif
        startPos = transform.position;

        UIGameController.Retry += ResetPlayer;
    }

    private void OnDestroy()
    {
        UIGameController.Retry -= ResetPlayer;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        yaw += speedH * Input.GetAxis("Mouse X")*Time.deltaTime;
        pitch -= speedV * Input.GetAxis("Mouse Y")*Time.deltaTime;

        transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0.0f);

        transform.parent.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        rig.velocity += transform.forward * ver * speed * Time.deltaTime;
        rig.velocity += transform.right * hor * speed * Time.deltaTime;
        rig.velocity = new Vector3(rig.velocity.x, 0.0f, rig.velocity.z);
#endif
        
    }

    public void ResetPlayer()
    {
        life = 200;

        transform.parent.position = startPos;

        ps.ResetPunches();
    }
}
