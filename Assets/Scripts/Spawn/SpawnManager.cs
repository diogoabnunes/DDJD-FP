using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private float awayDistance;
    [SerializeField] private float coolDown;

    [SerializeField] private int maxEnemiesActive;
    [SerializeField] private int minEnemiesToSpawn;
    [SerializeField] private int maxEnemiesToSpawn;

    List<GameObject> activeEnemies = new List<GameObject>();

    Transform player;

    float nextSpawnTime = 0f;

    SpawnRectangle spawnRectangle;

    void Start() {
        player = PlayerModel.instance.player.transform;
    }

    public void DefineSpawnRectangle(SpawnRectangle spawnRectangle) {
        this.spawnRectangle = spawnRectangle;
    }

    public void Spawn() {
        if (!CanSpawn()) return;

        int numEnemiesToSpawn = chooseNumberOfEnemiesToSpawn();

        int counter = 0;
        while (counter++ != numEnemiesToSpawn) {
            SpawnEnemy();
        }

        nextSpawnTime = Time.time + coolDown;
    }

    bool CanSpawn() {
        return Time.time >= nextSpawnTime && activeEnemies.Count < maxEnemiesActive;
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
        float x = Random.Range(spawnRectangle.getMinXPoint(), spawnRectangle.getMaxXPoint());
        float z = Random.Range(spawnRectangle.getMinZPoint(), spawnRectangle.getMaxZPoint());

        Vector3 point = new Vector3(x, spawnRectangle.getY(), z);

        return spawnRectangle.rotatePointToRectangleRotation(point);
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