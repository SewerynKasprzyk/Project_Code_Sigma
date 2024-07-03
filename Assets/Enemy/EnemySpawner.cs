using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f; // Time between spawns

    [SerializeField] private GameObject[] enemyPrefabs; // Enemy prefabs to spawn (multiple enemies)

    [SerializeField] private float spawnDistance = 5f; // Dystans, na którym spawner zaczyna dzia³aæ

    private Vector2 spawnRadius = new Vector2(2, 2); // Radius of the spawn area

    private GameObject player; // Referencja do gracza

    private bool isSpawning = false; // Is the spawner currently spawning?

    private void Start()
    {
        //StartCoroutine(Spawner());
        player = GameObject.FindWithTag("Player"); // ZnajdŸ gracza po tagu

    }

    private void Update()
    {
        if (player != null && !isSpawning && Vector2.Distance(transform.position, player.transform.position) <= spawnDistance)
        {
            StartCoroutine(Spawner());
            isSpawning = true; // Zapobiega wielokrotnemu uruchamianiu korutyny
        }
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
        //isSpawning = false; // Pozwala na ponowne uruchomienie korutyny, gdy gracz ponownie wejdzie w zasiêg

    }
}
