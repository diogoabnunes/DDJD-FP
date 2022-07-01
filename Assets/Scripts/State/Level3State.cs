using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3State : LevelState {
    int numberOfEnemiesToKill = 25;

    public override bool RequiredEnemiesDead() {
        return gameManager.GetNumberOfEnemiesKilled() >= numberOfEnemiesToKill;
    }

    public override State GetNextLevelBoss() {
        return new Level3BossState();
    }
    
    public override int GetDifficulty() {
        return 3;
    }
}