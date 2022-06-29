using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2State : LevelState {
    int numberOfEnemiesToKill = 1;

    public override bool LevelPassed() {
        return gameManager.GetNumberOfEnemiesKilled() >= numberOfEnemiesToKill;
    }

    public override State GetNextTransiction() {
        return new TransictionFromLevel2State();
    }

    public override int GetDifficulty() {
        return 2;
    }
}