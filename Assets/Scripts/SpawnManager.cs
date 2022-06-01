using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private float range;
    [SerializeField] private float awayDistance;
    [SerializeField] private float coolDown;

    [SerializeField] private int maxEnemiesActive;
    [SerializeField] private int minEnemiesToSpawn;
    [SerializeField] private int maxEnemiesToSpawn;

    List<GameObject> activeEnemies = new List<GameObject>();

    Transform player;

    float nextSpawnTime = 3f;

    void Start() {
        player = PlayerModel.instance.player.transform;
    }

    void Update()
    {
        if (CanSpawn()) {
            Spawn();
        }
    }

    bool CanSpawn() {
        return Time.time >= nextSpawnTime && activeEnemies.Count < maxEnemiesActive;
    }

    void Spawn() {
        int numEnemiesToSpawn = chooseNumberOfEnemiesToSpawn();
        int counter = 0;

        while (counter++ != numEnemiesToSpawn) {
            SpawnEnemy();
        }

        nextSpawnTime = Time.time + coolDown;
    }

    int chooseNumberOfEnemiesToSpawn() {
        int availableSpaceForEnemies = maxEnemiesActive - activeEnemies.Count;
        if (availableSpaceForEnemies <= minEnemiesToSpawn) {
            return availableSpaceForEnemies;
        }

        return Random.Range(minEnemiesToSpawn, Mathf.Min(availableSpaceForEnemies, maxEnemiesToSpawn) + 1);
    }

    void SpawnEnemy() {
        GameObject enemyToSpawn = chooseEnemyToSpawn();
        Vector3 pointToSpawn = choosePointToSpawn();

        GameObject enemy = Instantiate(enemyToSpawn, pointToSpawn, Quaternion.identity);
        activeEnemies.Add(enemy);
    }

    GameObject chooseEnemyToSpawn() {
        return enemies[Random.Range(0, enemies.Length)];
    }

    Vector3 choosePointToSpawn() {
        Vector3 point;

        do {
            point = generatePoint();
        } while (!validPoint(point));

        return point;
    }

    Vector3 generatePoint() {
        float x = Random.Range(player.position.x - range, player.position.x + range);
        float z = Random.Range(player.position.z - range, player.position.z + range);

        return new Vector3(x, 0, z);
    }

    bool validPoint(Vector3 point) {
        return awayFromPlayer(point) && awayFromEnemies(point);
    }

    bool awayFromPlayer(Vector3 point) {
        return Vector3.Distance(point, player.position) >= awayDistance;
    }

    bool awayFromEnemies(Vector3 point) {
        foreach (GameObject enemy in activeEnemies) {
            if (Vector3.Distance(point, enemy.GetComponent<Transform>().position) < awayDistance) {
                return false;
            }
        }

        return true;
    }

    public void enemyDied(GameObject enemy) {
        activeEnemies.Remove(enemy);
    }
}
