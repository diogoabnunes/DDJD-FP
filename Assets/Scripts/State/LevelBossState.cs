using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBossState : State {    
    public override void Setup() {
        
        SpawnBoss();
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        return null;
    }

    void SpawnBoss() {
        gameManager.spawnBoss();
    }

    public virtual State GetNextTransiction() {
        return null;
    }
}