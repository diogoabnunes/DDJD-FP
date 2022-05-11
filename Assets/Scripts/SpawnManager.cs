using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;

    [SerializeField] private float range;
    [SerializeField] private float coolDown;

    Transform transform;

    float nextSpawnTime = 0f;

    void Start() {
        transform = GetComponent<Transform>();
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
        GameObject enemyToSpawn = chooseEnemyToSpawn();

        float centerX = transform.position.x;
        float centerZ = transform.position.z;

        float x = Random.Range(centerX- range, centerX + range);
        float z = Random.Range(centerZ- range, centerZ + range);

        Instantiate(enemyToSpawn, new Vector3(x, 0, z), Quaternion.identity);

        nextSpawnTime = Time.time + coolDown;
    }

    GameObject chooseEnemyToSpawn() {
        int enemy = Random.Range(0, 3);

        if (enemy == 0) return enemy1;
        else if(enemy == 1) return enemy2;
        else return enemy3;
    }
}
