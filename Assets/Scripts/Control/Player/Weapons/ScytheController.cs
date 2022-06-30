using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ScytheController : WeaponController
{
    public GameObject Scythe;
    public ScytheExplosion scytheExplosion;

    public float damage = 10f;

    string[] verticalBasicAttackAnimationNames = {"attack2_phase1_vertical", "attack2_phase2_vertical", "attack2_phase3_vertical"};
    string[] horizontalBasicAttackAnimationNames = {"attack1_phase1_horizontal", "attack1_phase2_horizontal", "attack1_phase3_horizontal"};

    public VisualEffect slashVFX;
    
    int leftCurrentBasicAttackPhase = 0;

    int rightCurrentBasicAttackPhase = 0;

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

    public override void ExecuteLeftBasicAttack() {
        if (Time.time - timeSinceLastBasicAttack > comboTiming) {
            leftCurrentBasicAttackPhase = 0;
        }

        m_Animator.SetTrigger(horizontalBasicAttackAnimationNames[leftCurrentBasicAttackPhase]);
        slashVFX.Play();

        leftCurrentBasicAttackPhase = (leftCurrentBasicAttackPhase + 1) % horizontalBasicAttackAnimationNames.Length;
        timeSinceLastBasicAttack = Time.time;
    }

    public override void ExecuteRightBasicAttack() {
        if (Time.time - timeSinceLastBasicAttack > comboTiming) {
            rightCurrentBasicAttackPhase = 0;
        }

        m_Animator.SetTrigger(verticalBasicAttackAnimationNames[rightCurrentBasicAttackPhase]);
        slashVFX.Play();

        rightCurrentBasicAttackPhase = (rightCurrentBasicAttackPhase + 1) % verticalBasicAttackAnimationNames.Length;
        timeSinceLastBasicAttack = Time.time;
    }

    public override void ExecuteAbility1() {
        m_Animator.SetTrigger("dash");
        Vector3 moveDir = playerController.getCharacterFacingDirection();
        playerController.MovePlayer(moveDir.normalized * 30f * (1.5f / 3));
    }

    public override void ExecuteAbility2() {
        if(playerController.IsGrounded()){
            playerController.Jump();
            ExecuteRightBasicAttack();
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
            float realDamage = damage * playerModel.getPlayerModifiers().getDamageModifier();
            interactionManager.manageInteraction(new TakeDamage(realDamage, model));
        }

    }
}
