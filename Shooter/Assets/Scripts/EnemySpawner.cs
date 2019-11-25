using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefab;
    public int amount;
    Pool enemies;
    GameObject nextSpawned;

    public GameObject spawnPoints;
    GameObject[] spawnPoint;
    public float maxTime;
    public float timer;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new GameObject[spawnPoints.transform.childCount];
        EnemyBehaviour.ReturnEnemy = ReturnToPool;
        enemies = new Pool(amount, prefab, transform.position, transform);
        nextSpawned = enemies.GetActor();
        timer = maxTime;
        for(int i = 0;i<spawnPoints.transform.childCount;i++)
        {
            spawnPoint[i] = spawnPoints.transform.GetChild(i).gameObject;
        }

        UIGameController.Retry += RestartEnemies;
    }

    private void OnDestroy()
    {
        UIGameController.Retry -= RestartEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            nextSpawned = enemies.GetActor();
            nextSpawned.transform.position = spawnPoint[Random.Range(0, spawnPoints.transform.childCount)].transform.position;
            timer = maxTime;
        }
    }

    void ReturnToPool(GameObject enemy, int plusScore)
    {
        enemies.ReturnActorToPool(enemy);
        score += plusScore;
        if (score > 100)
            GooglePlayManager.gpm.UnlockAchievement();
    }

    void RestartEnemies()
    {
        score = 0;
        enemies.ResetPool();
    }
}
