using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionFromBossController : TransictionController
{
    public override int GetNextScene() {
        return 0;
    }

    public override State GetNextState() {
        return new MainMenuState();
    }
}
