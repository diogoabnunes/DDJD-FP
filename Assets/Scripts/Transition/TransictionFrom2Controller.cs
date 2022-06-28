using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransictionFrom2Controller : TransictionController
{
    public override int GetNextScene() {
        return 3;
    }

    public override State GetNextState() {
        return new Level3State();
    }
}
