using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2State : State {
    public int numberOfEnemiesToKill = 1;

    public override void Setup() {
        gameManager.ResetEnemyKilledCounter();
        gameManager.EnablePlayer();
        EnableBoss();
    }

    void EnableBoss() {
        GameObject boss = null, bossCanvas = null;

        var gameObjects = GameObject.FindObjectsOfType<GameObject>(true);
        foreach (GameObject gameObject in gameObjects) {
            if (gameObject.name == "Boss") {
                boss = gameObject;
                boss.SetActive(true);
            }
            else if (gameObject.name == "BossCanvas") {
                bossCanvas = gameObject;
                bossCanvas.SetActive(true);
            }
        }
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        if (LevelPassed()) {
            return new TransictionFromLevel2State();
        }

        return null;
    }

    bool LevelPassed() {
        return gameManager.GetNumberOfEnemiesKilled() >= numberOfEnemiesToKill;
    }
}