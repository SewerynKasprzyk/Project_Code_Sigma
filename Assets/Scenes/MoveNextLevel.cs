using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveNextLevel : MonoBehaviour
{
    public int sceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("Switching Scene");
            SceneManager.sceneLoaded += OnSceneLoaded; // Rejestrujemy nas³uchiwacz
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ustaw pozycjê gracza po za³adowaniu sceny
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
        if (spawnPoint != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = spawnPoint.transform.position;
            }
        }
        SceneManager.sceneLoaded -= OnSceneLoaded; // Wyrejestrowujemy nas³uchiwacz, aby nie by³ wywo³ywany przy ka¿dym ³adowaniu sceny
    }
}
