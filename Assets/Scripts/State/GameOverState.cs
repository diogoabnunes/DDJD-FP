using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State {
    float gameOverDelay = 4f;
    float startTime;

    GameObject gameOverMenu;

    bool enabled = false;

    public GameOverState() : base() {
        startTime = Time.time;

        var allObjects = GameObject.FindObjectsOfType<GameObject>(true);
        foreach(GameObject obj in allObjects) {
            if (obj.name == "GameOverMenu") {
                gameOverMenu = obj;
            }
        }

        gameManager.StopEnemies();
        gameManager.DisablePlayerInputController();
    }

    public override void Setup() {
    }

    public override State GetNextState() {
        if (CanShowGameOverMenu()) {
            EnableGameOverMenu();
        }

        return null;
    }

    bool CanShowGameOverMenu() {
        return DelayElapsed() && !enabled;
    }

    bool DelayElapsed() {
        return (Time.time - startTime) >= gameOverDelay;
    }

    void EnableGameOverMenu() {
        TimeAuxiliar.StopTime();
        gameManager.DisablePlayer();
        gameOverMenu.SetActive(true);

        enabled = true;
    }

    public override bool IsGameOver() {
        return true;
    }
}