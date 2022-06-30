using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2State : LevelState {
    int numberOfEnemiesToKill = 1;

    public override bool RequiredEnemiesDead() {
        return gameManager.GetNumberOfEnemiesKilled() >= numberOfEnemiesToKill;
    }

    public override State GetNextLevelBoss() {
        return new Level2BossState();
    }

    public override int GetDifficulty() {
        return 2;
    }
}