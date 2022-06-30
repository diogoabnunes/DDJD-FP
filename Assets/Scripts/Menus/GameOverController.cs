using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    GameManager gameManager;

    void Start() {
        gameManager = GameManager.instance;
    }

    public void Restart() {
        SceneManager.LoadScene(1);
        gameManager.ResetEnemyKilledCounter();
        PlayerModel.instance.Reset();
        gameManager.DisablePlayerInputController();
        gameManager.UpdateState(new Level1State());
    }

    public void MainMenu() {
        TimeAuxiliar.ResumeTime();
        SceneManager.LoadScene(6);
        gameManager.UpdateState(new MainMenuState());
    }

    public void Exit() {
        Application.Quit();
    }
}
