using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionFromLevel3State : TransictionState {
    public override void EnableTransition() {
        gameManager.EnableTransitionFrom3();
    }
}