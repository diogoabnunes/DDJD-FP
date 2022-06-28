using UnityEngine;

public class State {
    protected GameManager gameManager;

    public State() {
        this.gameManager = GameManager.instance;
    }

    public virtual void Setup() {}

    public virtual State GetNextState() {
        return null;
    }

    public virtual State GetPreviousState() {
        return null;
    }

    protected bool PauseMenuCommand() {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}