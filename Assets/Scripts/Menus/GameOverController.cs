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
        // gameManager.EnablePlayerGraphics();
        // gameManager.EnablePlayerCamera();
        gameManager.UpdateState(new Level1State());
        SceneManager.LoadScene(1);
    }

    public void MainMenu() {
        // gameManager.EnablePlayerGraphics();
        // gameManager.EnablePlayerCamera();
        gameManager.UpdateState(new MainMenuState());
        SceneManager.LoadScene(0);
    }

    public void Exit() {
        Application.Quit();
    }
}
