using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1State : LevelState {
    int numberOfEnemiesToKill = 1;

    public override bool LevelPassed() {
        return gameManager.GetNumberOfEnemiesKilled() >= numberOfEnemiesToKill;
    }

    public override State GetNextTransiction() {
        return new TransictionFromLevel1State();
    }
}