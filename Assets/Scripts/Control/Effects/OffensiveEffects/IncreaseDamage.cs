using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : OffensiveEffect
{
    PlayerModel playerModel;

    public IncreaseDamage(PlayerModel playerModel){
        this.playerModel = playerModel;
    }

    public override void execute(){
        
        
        playerModel.playerModifiers.increaseDamageModifier(0.1f);
    }
}
