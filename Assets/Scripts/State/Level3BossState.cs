using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3BossState : LevelBossState {
    public override State GetNextTransiction() {
        return new TransictionFromLevel3State();
    }
}