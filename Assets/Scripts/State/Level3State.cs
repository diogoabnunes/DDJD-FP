using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3State : State {
    public int numberOfEnemiesToKill = 1;

    public override void Setup() {
        TimeAuxiliar.ResumeTime();
        gameManager.ResetEnemyKilledCounter();
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        Debug.Log("level3 : " + gameManager.GetNumberOfEnemiesKilled());
        if (LevelPassed()) {
            return new TransictionFromLevel3State();
        }

        return null;
    }

    bool LevelPassed() {
        return gameManager.GetNumberOfEnemiesKilled() >= numberOfEnemiesToKill;
    }
}