using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1State : State {
    int numberOfEnemiesToKill = 1;

    public override void Setup() {
        gameManager.EnablePlayer();
        EnableSpawn();
    }

    void EnableSpawn() {
        GameObject spawn = null;

        var gameObjects = GameObject.FindObjectsOfType<GameObject>(true);
        foreach (GameObject gameObject in gameObjects) {
            if (gameObject.name == "SpawnManagerLevel1") {
                spawn = gameObject;
                spawn.SetActive(true);   
            }
        }
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        if (LevelPassed()) {
            return new TransictionFromLevel1State();
        }

        return null;
    }

    bool LevelPassed() {
        return gameManager.GetNumberOfEnemiesKilled() >= numberOfEnemiesToKill;
    }
}