using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float coolDown = 3f;
    float nextAttack = 0f;

    protected bool TimeElapsedForAttack() { 
        Debug.Log("Curr Time: " + Time.time);
        return nextAttack == 0 || nextAttack <= Time.time;
    }

    protected void DefineNextAttackTime() {
        nextAttack = Time.time + coolDown;
        Debug.Log("Next Attack: " + nextAttack);
    }

    public virtual bool CanAttack(float distanceToPlayer) {
        Debug.Log("Missing Implementation for: CanAttack()!");
        return false;
    }

    public virtual IEnumerator DoAttack() {
        Debug.Log("Missing Implementation for: Attack()!");
        yield return null;
    }
}
