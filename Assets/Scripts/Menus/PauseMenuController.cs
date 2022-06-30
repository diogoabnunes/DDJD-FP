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
        SceneManager.LoadScene(6);
        gameManager.UpdateState(new MainMenuState());
    }

    public void Exit() {
        Application.Quit();
    }
}
