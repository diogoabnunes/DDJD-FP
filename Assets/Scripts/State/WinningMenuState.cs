using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningMenuState : State {
    float gameOverDelay = 4f;
    float startTime;

    GameObject winningMenu;

    bool enabled = false;

    public WinningMenuState() : base() {
        startTime = Time.time;

        var allObjects = GameObject.FindObjectsOfType<GameObject>(true);
        foreach(GameObject obj in allObjects) {
            if (obj.name == "WinningMenu") {
                winningMenu = obj;
            }
        }
    }

    public override void Setup() {
    }

    public override State GetNextState() {
        if (CanShowWinningMenu()) {
            EnableWinningMenu();
        }

        return null;
    }

    bool CanShowWinningMenu() {
        return DelayElapsed() && !enabled;
    }

    bool DelayElapsed() {
        return (Time.time - startTime) >= gameOverDelay;
    }

    void EnableWinningMenu() {
        TimeAuxiliar.StopTime();
        gameManager.DisablePlayer();
        winningMenu.SetActive(true);

        enabled = true;
    }

    public override bool IsGameOver() {
        return true;
    }
}