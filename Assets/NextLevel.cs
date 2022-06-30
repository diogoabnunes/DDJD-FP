using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{

    GameManager gameManager;

    void Awake(){
        gameManager = GameManager.instance;
    }

    void OnTriggerEnter(Collider collider)
    {
         if(collider.name == "Third Person Player"){
            gameManager.BossLevelKilled();
        }
    }
}
