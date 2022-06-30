using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelState : State {
    public override void Setup() {
        TimeAuxiliar.ResumeTime();
        gameManager.ResetEnemyKilledCounter();
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        if (RequiredEnemiesDead()) {
            return GetNextLevelBoss();
        }

        return null;
    }

    public virtual bool RequiredEnemiesDead() {
        return false;
    }

    public virtual State GetNextLevelBoss() {
        return null;
    }

    public virtual int GetDifficulty() {
        return 1;
    }
}