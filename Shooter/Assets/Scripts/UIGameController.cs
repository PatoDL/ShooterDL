using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameController : MonoBehaviour
{
    public delegate void UICommand();
    public static UICommand Retry;

    public GameObject gamePanel;
    public GameObject retryPanel;

    public GameObject lifeBar;

    public GameController player;
    public EnemySpawner enemySpawner;

    public Text scoreText;

    RectTransform lifeBarT;

    float lifeBarOriginalWidth;

    // Start is called before the first frame update
    void Start()
    {

        lifeBarT = lifeBar.GetComponent<RectTransform>();
        lifeBarOriginalWidth = lifeBarT.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + enemySpawner.score;

        lifeBarT.sizeDelta = new Vector2(player.life,25);

        if(player.life <= 0)
        {
            gamePanel.SetActive(false);
            retryPanel.SetActive(true);
            GooglePlayManager.gpm.UploadScore(enemySpawner.score);
#if UNITY_STANDALONE || UNITY_EDITOR

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
#endif
        }
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        gamePanel.SetActive(true);
        retryPanel.SetActive(false);
#if UNITY_STANDALONE || UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif
        Retry();
    }
}
