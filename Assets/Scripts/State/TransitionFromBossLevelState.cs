using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionFromBossLevelState : TransictionState {
    public override void EnableTransition() {
        gameManager.EnableTransitionFromBoss();
    }
}