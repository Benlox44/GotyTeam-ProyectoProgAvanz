using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnInterval = 3f;

    void Start()
    {
        StartCoroutine(GenerateEnemiesRoutine());
    }

    private IEnumerator GenerateEnemiesRoutine() {
        while (true) {
            yield return new WaitForSeconds(spawnInterval);
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}