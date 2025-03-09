using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 8f;
    private int waveNumber = 1;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            for (int i = 0; i < waveNumber; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject tmp = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                tmp.GetComponent<Enemy>().ModifyEnemy(waveNumber);
                yield return new WaitForSeconds(0.3f);
            }

            waveNumber++;
            GameManager.Instance.GetGameplayCanvas().UpdateWave(waveNumber);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}