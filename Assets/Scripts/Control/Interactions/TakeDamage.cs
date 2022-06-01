using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : Interaction
{
    private Attack attack;
    private Character character;

    public TakeDamage(Attack attack, Character character){
        this.attack = attack;
        this.character = character;
    }

    public override void execute()
    {
        float realDamage = attack.GetAttackDamage();
        character.TakeDamage(realDamage);
    }
}
