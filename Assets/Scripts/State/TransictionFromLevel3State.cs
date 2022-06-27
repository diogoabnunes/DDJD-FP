using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionFromLevel3State : State {

    public override void Setup() {
        TimeAuxiliar.StopTime();
        // gameManager.DisablePlayer();
        gameManager.EnableTransitionFrom3();
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        return null;
    }
}