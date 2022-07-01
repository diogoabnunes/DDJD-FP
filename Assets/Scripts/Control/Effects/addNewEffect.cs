using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addNewEffect : MonoBehaviour
{
    List<Effect> effectList;

    PlayerModel playerModel;
    Effect objectEffect;

    private void Awake() {
        playerModel = PlayerModel.instance;
        
        effectList = new List<Effect>(){new IncreaseDamage(playerModel)};

        
        objectEffect = effectList[Random.Range(0, effectList.Count)];
    }

    void OnTriggerEnter(Collider collider)
    {
        
        if(collider.name == "Third Person Player"){

            
            objectEffect.execute();
            Destroy(gameObject);
        }
    }


}