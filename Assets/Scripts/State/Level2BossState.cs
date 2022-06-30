using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2BossState : LevelBossState {
    public override State GetNextTransiction() {
        return new TransictionFromLevel2State();
    }
}