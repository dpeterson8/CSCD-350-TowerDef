using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject currentEnemy) {
        yield return new WaitForSeconds(interval);

        GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemyPrefab));
    }
}
