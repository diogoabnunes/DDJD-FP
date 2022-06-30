using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelState : State {

    protected bool bossDefeated = false;
    protected bool bossActive = false;
    public override void Setup() {
        TimeAuxiliar.ResumeTime();
        gameManager.ResetEnemyKilledCounter();
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        if (RequiredEnemiesDead() && !bossActive && !bossDefeated) {
            SpawnBoss();
        }

        return null;
    }

    public virtual bool LevelPassed() {
        return false;
    }

    public virtual bool RequiredEnemiesDead() {
        return false;
    }

    public virtual void SpawnBoss() {
    }

    public virtual State GetNextTransiction() {
        return null;
    }
}