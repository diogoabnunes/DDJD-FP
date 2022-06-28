using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int numberOfEnemiesDead = 0;

    #region Singleton

    public static GameManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    public float getDifficulty(){
        return 1;
    }

    public void addEnemyKilled(){
        numberOfEnemiesDead += 1;
    }

    public int GetNumberOfEnemiesKilled() {
        return numberOfEnemiesDead;
    }

    public void ResetEnemyKilledCounter() {
        numberOfEnemiesDead = 0;
    }

    // ----------------------------------------------------------------

    State state;

    void Start() {
        state = new MainMenuState();
    }

    void Update() {
        State nextState = state.GetNextState();
        if (nextState != null) {
            UpdateState(nextState);
        }
    }

    public void UpdateState(State nextState) {
        state = nextState;
        state.Setup();
    }

    public void GoToPreviousState() {
        State previousState = state.GetPreviousState();
        if (previousState != null) {
            state = previousState;
        }
    }

    public void PlayerDied() {
        if (!state.IsGameOver()) {
            UpdateState(new GameOverState(Time.time));
        }
    }

    public void EnablePlayer() {
        EnablePlayerInputController();
        EnablePlayerCanvas();
    }

    public void EnablePlayerGraphics() {
        var gameObjects = GameObject.FindObjectsOfType<GameObject>(true);
        foreach (GameObject gameObject in gameObjects) {
            if (gameObject.name == "PlayerGFX") {
                gameObject.SetActive(true);
            }
        }
    }

    public void EnablePlayerCamera() {
        var gameObjects = GameObject.FindObjectsOfType<GameObject>(true);
        foreach (GameObject gameObject in gameObjects) {
            if (gameObject.name == "Third Person Camera") {
                gameObject.SetActive(true);
            }
        }
    }

    void EnablePlayerInputController() {
        GameObject playerInput = GameObject.Find("Third Person Player");
        if (playerInput != null) {
            playerInput.GetComponent<PlayerController>().Enable();
        }
    }

    void EnablePlayerCanvas() {
        var gameObjects = GameObject.FindObjectsOfType<GameObject>(true);
        foreach (GameObject gameObject in gameObjects) {
            if (gameObject.name == "PlayerCanvas") {
                gameObject.SetActive(true);
            }
        }
    }

    public void DisablePlayer() {
        DisablePlayerInputController();
        DisablePlayerCanvas();
    }

    void DisablePlayerInputController() {
        GameObject playerInput = GameObject.Find("Third Person Player");
        if (playerInput != null) {
            playerInput.GetComponent<PlayerController>().Disable();
        }
    }

    void DisablePlayerCanvas() {
        GameObject playerCanvas = GameObject.Find("PlayerCanvas");
        if (playerCanvas != null) {
            playerCanvas.SetActive(false);
        }
    }

    public void DestroyEnemies() {
        var enemies = FindObjectsOfType<EnemyController>();
        foreach (var enemy in enemies) {
            Destroy(enemy.gameObject);
        }
    }

    public void EnableTransitionFrom1() {
        var spawn = GameObject.Find("TransitionCanvas").transform.Find("TransictionFrom1");
        if (spawn != null) {
            spawn.gameObject.SetActive(true);
        }
    }

    public void EnableTransitionFrom2() {
        var spawn = GameObject.Find("TransitionCanvas").transform.Find("TransictionFrom2");
        if (spawn != null) {
            spawn.gameObject.SetActive(true);
        }
    }

    public void EnableTransitionFrom3() {
        var spawn = GameObject.Find("TransitionCanvas").transform.Find("TransictionFrom3");
        if (spawn != null) {
            spawn.gameObject.SetActive(true);
        }
    }

    public void EnableTransitionFromBoss() {
        var spawn = GameObject.Find("TransitionCanvas").transform.Find("TransictionFromBoss");
        if (spawn != null) {
            spawn.gameObject.SetActive(true);
        }
    }
}
