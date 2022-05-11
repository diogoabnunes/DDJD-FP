using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;

    [SerializeField] private float range;
    [SerializeField] private float awayDistanceFromPlayer;
    [SerializeField] private float coolDown;

    Transform player;

    float nextSpawnTime = 3f;

    void Start() {
        player = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        if (CanSpawn()) {
            Spawn();
        }
    }

    bool CanSpawn() {
        return Time.time >= nextSpawnTime;
    }

    void Spawn() {
        int numEnemiesToSpawn = Random.Range(1, 4);
        int counter = 0;

        while (counter != numEnemiesToSpawn) {
            SpawnEnemy();
            
            counter++;
        }

        nextSpawnTime = Time.time + coolDown;
    }

    void SpawnEnemy() {
        GameObject enemyToSpawn = chooseEnemyToSpawn();
        Vector3 pointToSpawn = choosePointToSpawn();

        Instantiate(enemyToSpawn, pointToSpawn, Quaternion.identity);
    }

    GameObject chooseEnemyToSpawn() {
        int enemy = Random.Range(0, 3);

        if (enemy == 0) return enemy1;
        else if(enemy == 1) return enemy2;
        else return enemy3;
    }

    Vector3 choosePointToSpawn() {
        float playerX = player.position.x;
        float playerZ = player.position.z;

        float x = generateCoordinateAwayFromPlayerToSpawn(playerX);
        float z = generateCoordinateAwayFromPlayerToSpawn(playerZ);
        while (!validPosition(x, z)) {
            x = generateCoordinateAwayFromPlayerToSpawn(playerX);
            z = generateCoordinateAwayFromPlayerToSpawn(playerZ);
        }

        return new Vector3(x, 0, z);
    }

    bool validPosition(float x, float z) {
        // verify if it's not already an enemy position
        return true;
    }

    float generateCoordinateAwayFromPlayerToSpawn(float center) {
        float coordinate = Random.Range(center - range, center + range);
        
        while (!awayFromPlayer(coordinate, center)) {
            coordinate = Random.Range(center - range, center + range);
        }

        return coordinate;
    }

    bool awayFromPlayer(float point, float player) {
        return Mathf.Abs(point - player) >= awayDistanceFromPlayer;
    }
}
