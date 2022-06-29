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
        Debug.Log(playerModel);
        effectList = new List<Effect>(){new IncreaseDamage(playerModel)};

        
        objectEffect = effectList[Random.Range(0, effectList.Count)];
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name);
        if(collider.name == "Third Person Player"){

            Debug.Log(collider.name);
            objectEffect.execute();
            Destroy(gameObject);
        }
    }


}