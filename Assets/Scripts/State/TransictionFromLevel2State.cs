using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionFromLevel2State : TransictionState {
    public override void EnableTransition() {
        gameManager.EnableTransitionFrom2();
    }
}