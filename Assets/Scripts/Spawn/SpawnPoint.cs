using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public SpawnManager spawnManager;

    Vector3 position;
    float range;

    float lastSpawn = 4f;

    SpawnRectangle spawnRectangle;
    bool spawn = false;

    void Start() {
        Vector3 position = transform.position;
        position.y = 1 * transform.localScale.y;

        spawnRectangle = new SpawnRectangle(position, transform.localScale.x, transform.localScale.z, transform.rotation.y);
    }
    
    void Update() {
        if (Time.time - lastSpawn > 3f) {
            spawnManager.DefineSpawnRectangle(spawnRectangle);
            spawnManager.Spawn();
        }
    }

    void OnTriggerEnter() {
        // spawnManager.DefineSpawnPoint(position, 3f);
        // spawnManager.Spawn();
    }
}
