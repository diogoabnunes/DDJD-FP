using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float coolDown = 3f;
    float nextAttack = 0f;

    protected InteractionManager interactionManager;

    public virtual void Start() {
        interactionManager = InteractionManager.instance;
    }

    protected bool TimeElapsedForAttack() {
        return nextAttack == 0 || nextAttack <= Time.time;
    }

    protected void DefineNextAttackTime() {
        nextAttack = Time.time + coolDown;
    }

    public virtual bool CanAttack(float distanceToPlayer) {
        
        return false;
    }

    public void DoAttack() {
        StartCoroutine(DoAttackCoroutine());
    }

    public virtual IEnumerator DoAttackCoroutine() {
        
        yield return null;
    }

    public virtual float GetAttackDamage(){
      return 1f;
    }
}
