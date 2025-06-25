using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign enemy prefab in Inspector
    public Transform spawnPoint; // Assign spawn point in Inspector
    public float respawnTime = 3f; // Time before respawn

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void EnemyDied()
    {
        Debug.Log("Enemy died. Respawning...");
        Invoke(nameof(SpawnEnemy), respawnTime); // Delay spawn
    }
}
