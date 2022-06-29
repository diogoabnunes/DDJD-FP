using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionFrom1Controller : TransictionController
{
    public override int GetNextScene() {
        return 2;
    }

    public override State GetNextState() {
        return new Level2State();
    }
}
