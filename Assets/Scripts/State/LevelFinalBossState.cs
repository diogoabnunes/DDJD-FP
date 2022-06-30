using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinalBossState : State {
    int numberOfEnemiesToKill = 1;

    public override void Setup() {
        TimeAuxiliar.ResumeTime();
        gameManager.ResetEnemyKilledCounter();
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        if (RequiredEnemiesDead()) {
            return new WinningMenuState();
        }

        return null;
    }

    bool RequiredEnemiesDead() {
        return gameManager.GetNumberOfEnemiesKilled() >= numberOfEnemiesToKill;
    }
}