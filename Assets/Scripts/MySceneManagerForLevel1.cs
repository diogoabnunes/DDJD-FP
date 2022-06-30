using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneManagerForLevel1 : MonoBehaviour
{
    void Start() {
        CheckForDuplicatedObjects("Main Camera");
        CheckForDuplicatedObjects("Third Person Player");
        CheckForDuplicatedObjects("PlayerCanvas");
        CheckForDuplicatedObjects("Third Person Camera");
        CheckForDuplicatedObjects("Sound");
    }

    void CheckForDuplicatedObjects(string name) {
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>(true);
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
