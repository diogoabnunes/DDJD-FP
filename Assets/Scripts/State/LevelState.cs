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

        if (LevelPassed()) {
            return GetNextTransiction();
        }

        return null;
    }

    public virtual bool LevelPassed() {
        return false;
    }

    public virtual State GetNextTransiction() {
        return null;
    }
}