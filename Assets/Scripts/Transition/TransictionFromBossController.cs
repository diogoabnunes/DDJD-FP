using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionFromBossController : TransictionController
{
    public override int GetNextScene() {
        return 5;
    }

    public override State GetNextState() {
        return new CreditsState();
    }
}
