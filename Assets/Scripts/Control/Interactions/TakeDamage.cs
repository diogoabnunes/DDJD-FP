using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : Interaction
{
    private float damage;

    System.Random random;
    private CharacterModel character;

    public TakeDamage(float damage, CharacterModel character){
        random = new System.Random();
        this.damage = damage;
        this.character = character;
    }

    public override void execute()
    {
        float rand = random.Next(7,12) / 10.0f;
        float realDamage = damage * rand;
        Debug.Log(realDamage);
        character.TakeDamage(realDamage);
    }
}
