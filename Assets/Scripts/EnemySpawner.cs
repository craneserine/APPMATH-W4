using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public Transform cubicSpawnPoint;
    public Transform quadraticSpawnPoint;
    public Transform endPoint;

    public Transform cubicControlPoint1; // First control point for cubic Bezier
    public Transform cubicControlPoint2; // Second control point for cubic Bezier
    public Transform quadraticControlPoint; // Control point for quadratic Bezier

    public float spawnInterval = 3f;
    public float delayBetweenEnemies = 1f;

    private void Start()
    {
        if (enemy1Prefab != null && enemy2Prefab != null && cubicSpawnPoint != null &&
            quadraticSpawnPoint != null && endPoint != null && cubicControlPoint1 != null &&
            cubicControlPoint2 != null && quadraticControlPoint != null)
        {
            InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
        }
        else
        {
            Debug.LogWarning("Some required fields are not assigned in the Inspector!");
        }
    }

    private void SpawnEnemy()
    {
        Debug.Log("Spawning enemies");

        GameObject selectedEnemy1 = Random.Range(0, 2) == 0 ? enemy1Prefab : enemy2Prefab;
        GameObject selectedEnemy2 = Random.Range(0, 2) == 0 ? enemy1Prefab : enemy2Prefab;

        GameObject enemy1 = Instantiate(selectedEnemy1, cubicSpawnPoint.position, Quaternion.identity);
        EnemyBehavior enemy1Behavior = enemy1.GetComponent<EnemyBehavior>();
        enemy1Behavior.startPoint = cubicSpawnPoint;
        enemy1Behavior.endPoint = endPoint;
        enemy1Behavior.useCubicLerp = true;
        enemy1Behavior.cubicControlPoint1 = cubicControlPoint1;
        enemy1Behavior.cubicControlPoint2 = cubicControlPoint2;

        StartCoroutine(SpawnSecondEnemy(selectedEnemy2));
    }

    private IEnumerator SpawnSecondEnemy(GameObject selectedEnemy2)
    {
        yield return new WaitForSeconds(delayBetweenEnemies);

        GameObject enemy2 = Instantiate(selectedEnemy2, quadraticSpawnPoint.position, Quaternion.identity);
        EnemyBehavior enemy2Behavior = enemy2.GetComponent<EnemyBehavior>();
        enemy2Behavior.startPoint = quadraticSpawnPoint;
        enemy2Behavior.endPoint = endPoint;
        enemy2Behavior.useCubicLerp = false;
        enemy2Behavior.quadraticControlPoint = quadraticControlPoint;
    }
}
