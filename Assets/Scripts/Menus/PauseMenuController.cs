using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    GameManager gameManager;

    void Start() {
        gameManager = GameManager.instance;
    }

    public void Resume() {
        TimeAuxiliar.ResumeTime();
        this.gameObject.SetActive(false);
        gameManager.GoToPreviousState();
        gameManager.EnablePlayer();
    }

    public void MainMenu() {
        TimeAuxiliar.ResumeTime();
        gameManager.UpdateState(new MainMenuState());
        SceneManager.LoadScene(0);
    }

    public void Exit() {
        Application.Quit();
    }
}
