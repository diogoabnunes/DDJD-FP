using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelState : LevelState {
    int numberOfEnemiesToKill = 1;

    public override bool LevelPassed() {
        return gameManager.GetNumberOfEnemiesKilled() >= numberOfEnemiesToKill;
    }

    public override State GetNextTransiction() {
        return new WinningMenuState();
    }
}