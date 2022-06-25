using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ScytheController : WeaponController
{
    public GameObject Scythe;
    public ScytheExplosion scytheExplosion;

    public float damage = 10f;

    string[] basicAttackAnimationNames = {"attack1_phase1", "attack1_phase2", "attack1_phase3"};

    public VisualEffect slashVFX;
    
    int currentBasicAttackPhase = 0;

    float timeSinceLastBasicAttack = 0f;
    public float comboTiming = 2f;


    override public void Start() {
        base.Start();

        Scythe.SetActive(true);
    }

    public override void Enable() {
        Scythe.SetActive(true);
    }

    public override void Disable() {
        Scythe.SetActive(false);
    }

    public override void ExecuteBasicAttack() {
        if (Time.time - timeSinceLastBasicAttack > comboTiming) {
            currentBasicAttackPhase = 0;
        }

        m_Animator.SetTrigger(basicAttackAnimationNames[currentBasicAttackPhase]);
        slashVFX.Play();

        currentBasicAttackPhase = (currentBasicAttackPhase + 1) % basicAttackAnimationNames.Length;
        timeSinceLastBasicAttack = Time.time;
    }

    public override void ExecuteAbility1() {
        Vector3 moveDir = playerController.getCharacterFacingDirection();
        playerController.MovePlayer(moveDir.normalized * 30f * (1.5f / 3));
    }

    public override void ExecuteAbility2() {
        if(playerController.IsGrounded()){
            playerController.Jump();
            ExecuteBasicAttack();
        }
        else{
            playerController.Dive();

            Collider[] hitColliders = scytheExplosion.ExplosionDamage(playerController.GetCharacterGlobalPosition());

            TriggerAOEDamage(hitColliders, scytheExplosion.damage);
        }
        
    }

    private void TriggerAOEDamage(Collider[] hitColliders, float aoeDamage){
        Debug.Log("dealing " + aoeDamage + " to " + hitColliders.Length + " monsters");
        foreach (var hitCollider in hitColliders)
        {
           CharacterModel model = hitCollider.gameObject.GetComponent<CharacterModel>();
            if (model != null) {
                interactionManager.manageInteraction(new TakeDamage(aoeDamage, model));
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!IsLocked() || other.gameObject.tag == "Player") return;

        CharacterModel model = other.gameObject.GetComponent<CharacterModel>();
        if (model != null) {
            interactionManager.manageInteraction(new TakeDamage(damage, model));
        }

    }
}
