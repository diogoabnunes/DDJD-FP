using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionFromBossState : State {

    public override void Setup() {
        TimeAuxiliar.StopTime();
        gameManager.EnableTransitionFromBoss();
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        return null;
    }
}