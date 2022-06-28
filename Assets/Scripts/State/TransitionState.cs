using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionState : State {

    public override void Setup() {
        TimeAuxiliar.StopTime();
        EnableTransition();
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            return new PauseMenuState(this);
        }

        return null;
    }

    public virtual void EnableTransition() {}
}