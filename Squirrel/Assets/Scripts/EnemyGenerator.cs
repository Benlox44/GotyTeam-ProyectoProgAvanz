using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxEnemies = 10;

    private int currentEnemies = 0;

    void Start()
    {
        StartCoroutine(GenerateEnemiesRoutine());
    }

    private IEnumerator GenerateEnemiesRoutine() {
        while (true) {
            if (currentEnemies < maxEnemies)
            {
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                currentEnemies++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void EnemyDied() {
        currentEnemies--;
    }
}