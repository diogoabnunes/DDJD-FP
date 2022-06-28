using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2State : State {
    public int numberOfEnemiesToKill = 1;

    public override void Setup() {
        TimeAuxiliar.ResumeTime();
        gameManager.ResetEnemyKilledCounter();
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        Debug.Log("level2 : " + gameManager.GetNumberOfEnemiesKilled());
        if (LevelPassed()) {
            return new TransictionFromLevel2State();
        }

        return null;
    }

    bool LevelPassed() {
        return gameManager.GetNumberOfEnemiesKilled() >= numberOfEnemiesToKill;
    }
}