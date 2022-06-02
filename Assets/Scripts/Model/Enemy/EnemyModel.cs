using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyModel : CharacterModel
{
    protected SpawnManager spawnManager = null;

    protected GameManager gameManager;

    public bool dead = false;

    public virtual void Start() {
      gameManager = GameManager.instance;

      SpawnManager[] obj = FindObjectsOfType<SpawnManager>();
      if (obj.Length != 0) {
          spawnManager = obj[0];
      }
    }

    public virtual void Die() {
      if (spawnManager != null){
          spawnManager.enemyDied(this.gameObject);
      }

      Destroy(gameObject);
    }
}
