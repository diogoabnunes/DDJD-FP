using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionFromLevel2State : State {

    public override void Setup() {
        TimeAuxiliar.StopTime();
        gameManager.EnableTransitionFrom2();
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        return null;
    }
}