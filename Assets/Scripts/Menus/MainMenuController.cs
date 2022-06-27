using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    GameManager gameManager;

    void Start() {
        gameManager = GameManager.instance;
    }

    public void Play() {
        // gameManager.EnablePlayerGraphics();
        // gameManager.EnablePlayerCamera();
        gameManager.UpdateState(new Level1State());
        SceneManager.LoadScene(1);
    }

    public void Exit() {
        Application.Quit();
    }
}
