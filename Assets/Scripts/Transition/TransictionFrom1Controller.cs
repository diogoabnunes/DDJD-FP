using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransictionFrom1Controller : MonoBehaviour
{
    Image image;

    float lastUpdate;
    float increment;

    float MAX_ALPHA = 1f;
    float MIN_ALPHA = 0f;

    GameManager gameManager;

    void Start() {
        image = GetComponent<Image>();

        lastUpdate = Time.unscaledTime;
        increment = 1f / 40f;

        gameManager = GameManager.instance;
    }

    void Update() {
        if (Time.unscaledTime - lastUpdate >= increment) {
            UpdateTransition();
        }

        if (ReachedMiddleOfTransition()) {
            increment = -increment;
            SetupNextLevel();
        }

        if (ReachedEndOfTransition()) {
            gameManager.UpdateState(new Level2State());
            this.gameObject.SetActive(false);
        }
    }

    bool ReachedMiddleOfTransition() {
        return image.color.a == MAX_ALPHA;
    }

    bool ReachedEndOfTransition() {
        return image.color.a == MIN_ALPHA && increment < 0;
    }

    void UpdateTransition() {
        lastUpdate = Time.unscaledTime;

        float newAlpha = image.color.a + increment;

        newAlpha = Mathf.Min(newAlpha, MAX_ALPHA);
        newAlpha = Mathf.Max(newAlpha, MIN_ALPHA);

        image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
    }

    void SetupNextLevel() {
        gameManager.DestroyEnemies();
        TimeAuxiliar.ResumeTime();
        SceneManager.LoadScene(1);
    }
}
