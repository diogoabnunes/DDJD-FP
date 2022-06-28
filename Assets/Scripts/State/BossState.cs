using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : State {
    public int numberOfEnemiesToKill = 1;

    public override void Setup() {
        TimeAuxiliar.ResumeTime();
        gameManager.ResetEnemyKilledCounter();
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        if (LevelPassed()) {
            return new WinningMenuState();
        }

        return null;
    }

    bool LevelPassed() {
        return gameManager.GetNumberOfEnemiesKilled() >= numberOfEnemiesToKill;
    }
}