using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDamageArea : MonoBehaviour
{
    float startTime = 0;

    void Start() {
        startTime = Time.time;
    }

    void Update() {
        if (Time.time - startTime >= 4f) {
            Destroy(this.gameObject);
        }
    }
}
