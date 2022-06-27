using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransictionFrom2Controller : MonoBehaviour
{
    Image image;

    float lastUpdate;
    float increment;

    float MAX_ALPHA = 1f;
    float MIN_ALPHA = 0f;

    GameManager gameManager;

    void Start() {
        image = GetComponent<Image>();

        lastUpdate = Time.time;
        increment = 1f / 40f;

        gameManager = GameManager.instance;
    }

    void Update() {
        if (Time.time - lastUpdate >= increment) {
            lastUpdate = Time.time;

            float newAlpha = image.color.a + increment;

            newAlpha = Mathf.Min(newAlpha, MAX_ALPHA);
            newAlpha = Mathf.Max(newAlpha, MIN_ALPHA);

            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
        }

        if (image.color.a == MAX_ALPHA) {
            increment = -increment;

            gameManager.DestroyEnemies();
            TimeAuxiliar.ResumeTime();
        }

        if (image.color.a == MIN_ALPHA && increment < 0) {
            // gameManager.UpdateState(new Level2State());
            this.gameObject.SetActive(false);
        }
    }
}
