using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuState : State {
    State previousState;

    GameObject pauseMenu;

    public PauseMenuState(State previousState) {
        this.previousState = previousState;
        
        var allObjects = GameObject.FindObjectsOfType<GameObject>(true);
        foreach(GameObject obj in allObjects) {
            if (obj.name == "PauseMenu") {
                pauseMenu = obj;
            }
        }

        Activate();
    }

    void Activate() {
        TimeAuxiliar.StopTime();
        gameManager.DisablePlayer();
        pauseMenu.SetActive(true);
    }

    void Disactivate() {
        pauseMenu.SetActive(false);
        gameManager.EnablePlayer();
        TimeAuxiliar.ResumeTime();
    }

    public override State GetNextState() {
        if (PauseMenuCommand()) {
            Disactivate();
            return previousState;
        }
        
        return null;
    }

    public override State GetPreviousState() {
        return previousState;
    }
}