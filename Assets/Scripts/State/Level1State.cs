using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1State : State {
    int numberOfEnemiesToKill = 1;

    public override void Setup() {
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
        Debug.Log(gameManager.GetNumberOfEnemiesKilled());
        return gameManager.GetNumberOfEnemiesKilled() >= numberOfEnemiesToKill;
    }
}