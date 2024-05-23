using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f; // Time between spawns

    [SerializeField] private GameObject[] enemyPrefabs; // Enemy prefabs to spawn (multiple enemies)

    [SerializeField] private bool canSpawn = true; // Can the spawner spawn enemies?

    private Vector2 spawnRadius = new Vector2(2, 2); // Radius of the spawn area

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner() { 
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        //while (canSpawn)
        for(int i = 0; i < 5; i++)
        {
            yield return wait;
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];

            Vector2 spawnPosition = (Random.insideUnitCircle * spawnRadius) + (Vector2)transform.position;
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity); // Spawn enemy
        }

    }
}
