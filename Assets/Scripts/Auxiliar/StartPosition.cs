using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.Find("Third Person Player");
        if (player != null) {
            Debug.Log("Setting Player Position");
            player.GetComponent<Transform>().position = transform.position;
        }
    }
}
