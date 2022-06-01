using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : Interaction
{
    private float damage;
    private CharacterModel character;

    public TakeDamage(float damage, CharacterModel character){
        this.damage = damage;
        this.character = character;
    }

    public override void execute()
    {
        float realDamage = damage;
        character.TakeDamage(realDamage);
    }
}
