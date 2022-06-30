using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TakeDamage : Interaction
{
    private float damage;
    System.Random random;
    private CharacterModel character;
    bool applyMath = true;

    public TakeDamage(float damage, CharacterModel character){
        random = new System.Random();
        this.damage = damage;
        this.character = character;
    }

    public TakeDamage(float damage, CharacterModel character, bool applyMath){
        random = new System.Random();
        this.damage = damage;
        this.character = character;
        this.applyMath = applyMath;
    }

    public override void execute()
    {
        int realDamage = (int) damage;

        if (applyMath) {
            float rand = random.Next((int) (damage*0.7f),(int) (damage*1.2f)) / 10.0f;
            realDamage = (int)Math.Ceiling(damage * rand);
        }

        character.TakeDamage(realDamage);
    }
}
