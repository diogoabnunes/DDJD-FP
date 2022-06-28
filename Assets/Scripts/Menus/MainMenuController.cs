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
        SceneManager.LoadScene(1);
        gameManager.UpdateState(new Level1State());
    }

    public void Exit() {
        Application.Quit();
    }
}
