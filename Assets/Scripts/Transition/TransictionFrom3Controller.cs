using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionFrom3Controller : TransictionController
{
    public override int GetNextScene() {
        return 4;
    }

    public override State GetNextState() {
        return new BossState();
    }
}
