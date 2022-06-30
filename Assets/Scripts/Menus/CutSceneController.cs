using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    
    public GameObject skipButton;
    public float timeElapsedToShowSkipButton = 3f;

    bool buttonIsActive = false;

    bool startedPlayingFirstTime = false;

    void Update() {
        if (VideoIsPlaying()) {
            startedPlayingFirstTime = true;
        }

        if (CanShowButton()) {
            EnableButton();
        }

        if (VideoIsOver()) {
            GoToNextScene();
        }
    }

    bool CanShowButton() {
        return videoPlayer.time >= timeElapsedToShowSkipButton && !buttonIsActive;
    }

    void EnableButton() {
        skipButton.SetActive(true);
        buttonIsActive = true;
    }

    bool VideoIsOver() {
        return !videoPlayer.isPlaying && startedPlayingFirstTime;
    }

    bool VideoIsPlaying() {
        return videoPlayer.isPlaying;
    }

    public void Skip() {
        GoToNextScene();
    }

    void GoToNextScene() {
        SceneManager.LoadScene(6);
    }
}
