using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModifiers
{
    public float damageModifier = 1.0f;
    public float lifeStealModifier = 0.0f;

    public void increaseDamageModifier(float increment){
        damageModifier += increment;
    }

    public void increaseLifeStealModifier(float increment){
        lifeStealModifier += increment;
    }

    public void decreaseDamageModifier(float increment){
        damageModifier -= increment;
    }

    public void decreaseLifeStealModifier(float increment){
        lifeStealModifier -= increment;
    }

    public float getDamageModifier(){
        return damageModifier;
    }

    public float getLifeStealModifier(){
        return lifeStealModifier;
    }
}