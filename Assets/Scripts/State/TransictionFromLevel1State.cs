using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionFromLevel1State : TransictionState {
    public override void EnableTransition() {
        gameManager.EnableTransitionFrom1();
    }
}