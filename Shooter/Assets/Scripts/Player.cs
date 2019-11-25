using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameController gc;

    private void Start()
    {
        gc = GetComponentInChildren<GameController>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            gc.life -= 10;
            if(gc.life<=0)
            {
                Time.timeScale = 0f;
                AddManager.instance.UIWatchAd();
            }
        }
    }
}
