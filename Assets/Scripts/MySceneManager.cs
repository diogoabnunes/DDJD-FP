using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    void Start() {
        CheckForDuplicatedObjects("GameManager");
        CheckForDuplicatedObjects("TransitionCanvas");
    }

    void CheckForDuplicatedObjects(string name) {
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> duplicated = new List<GameObject>();
        foreach (GameObject obj in objects) {
            if (obj.name == name) {
                duplicated.Add(obj);
            }
        }

        while (duplicated.Count > 1) {
            Destroy(duplicated[0]);
            duplicated.RemoveAt(0);
        }
    }
}
