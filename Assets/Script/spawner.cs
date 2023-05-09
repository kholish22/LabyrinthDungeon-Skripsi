using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
     public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 3f;
    public float detectionRange = 5f;
    public int maxEnemies = 5;

    private float timeSinceLastSpawn;
    private int numEnemies;

    private void Update()
    {
        // Mendapatkan posisi player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
       
        if (player != null) {
             Vector3 playerPosition = player.transform.position;

            // Menghitung jarak antara spawner dan player
            float distance = Vector3.Distance(transform.position, playerPosition);
            // Mendeteksi apakah player berada dalam jarak deteksi
                 if (Vector3.Distance(transform.position, playerPosition) < detectionRange && numEnemies < maxEnemies)
             {
                    // Menghitung interval spawn
                    timeSinceLastSpawn += Time.deltaTime;

                    // Spawning enemy
                    if (timeSinceLastSpawn >= spawnInterval)
                    {
                        // Memilih spawn point secara acak
                        int spawnIndex = Random.Range(0, spawnPoints.Length);
                        Transform spawnPoint = spawnPoints[spawnIndex];

                        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                        timeSinceLastSpawn = 0f;
                        numEnemies++;
                    }
             }
        }
    }

    // Method untuk dikaitkan dengan event trigger pada pintu masuk ruangan
    public void OnPlayerEnterRoom()
    {
        // Mulai spawn enemy
        enabled = true;
    }

    // Method untuk dikaitkan dengan event trigger pada pintu keluar ruangan
    public void OnPlayerExitRoom()
    {
        // Berhenti spawn enemy
        enabled = false;
    }

    // Method untuk dikaitkan dengan event trigger pada enemy yang mati
    public void OnEnemyDeath()
    {
        numEnemies--;
    }

    private void OnDestroy()
    {
        // Menghentikan spawn enemy
        enabled = false;
    }

}
