using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBorn : MonoBehaviour
{
    private float timer = 0f;
    public float spawnInterval = 30f; // Spawn interval in seconds
    public GameObject enemyPrefab; // Reference to the enemy prefab

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        // Code to spawn a new enemy goes here
        //在一个固定范围内随机生成敌人
        Vector3 spawnPosition = new Vector3(Random.Range(-34f, 58f), 0.5f, Random.Range(-4f, 77f));
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EEFManager.instance.BornAudio();
    }
}
