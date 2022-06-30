using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{

    GameManager gameManager;

    void Awake(){
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
         if(collider.name == "Third Person Player"){

            Debug.Log(collider.name);

            gameManager.UpdateState(new TransictionFromLevel3State());
        }
    }
}
