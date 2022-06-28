using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : State {

    public GameOverState() : base() {
        TimeAuxiliar.StopTime();
        gameManager.DisablePlayer();
        SceneManager.LoadScene(5);
    }

    public override void Setup() {
    }

    public override State GetNextState() {        
        return null;
    }
}