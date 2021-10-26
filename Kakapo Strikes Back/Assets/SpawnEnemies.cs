using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject friendlyObstacle;
    [SerializeField] private KillToPassDisplay killToPass;

    #region Data
    private float maxX = 3.5f;
    private float minX = 3f;
    private float maxY = 4.5f;
    private float minY = -4.5f;
    private float timeBetweenEnemySpawn = 2f;
    private float timeBetweenFriendSpawn = 5f;
    private float spawnTime;
    private float spawnFriend;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (killToPass.KillToPassValue <= 2)
            return;
        else
        {
            if(Time.time > spawnTime)
            {
                foreach (GameObject enemy in enemies)
                {
                    SpawnEnemy(enemy);
                    spawnTime = Time.time + timeBetweenEnemySpawn;
                }
            }

            if (Time.time > spawnFriend)
            {
                SpawnFriendlyObstacle();
                spawnFriend = Time.time + timeBetweenFriendSpawn;
            }
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Instantiate(enemy, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }

    private void SpawnFriendlyObstacle()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Instantiate(friendlyObstacle, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }
}

