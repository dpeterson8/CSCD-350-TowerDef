using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private float spawnInterval;

    public int maxEnemies = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject currentEnemy) {
        yield return new WaitForSeconds(interval);

        if(maxEnemies > 0) {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.transform.position , Quaternion.identity);
            StartCoroutine(spawnEnemy(interval, enemyPrefab));
            maxEnemies--;
        }
        // GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.transform.position , Quaternion.identity);
        // StartCoroutine(spawnEnemy(interval, enemyPrefab));
    }
}
