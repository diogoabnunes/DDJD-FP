using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using FMODUnity;

public class ScytheController : WeaponController
{
    public GameObject Scythe;
    public Explosion scytheExplosion;

    public GameObject dashHitbox;

    public float damage = 10f;

    [EventRef, SerializeField] string scytheSlash = default;

    string[] verticalBasicAttackAnimationNames = {"attack2_phase1_vertical", "attack2_phase2_vertical", "attack2_phase3_vertical"};
    string[] horizontalBasicAttackAnimationNames = {"attack1_phase1_horizontal", "attack1_phase2_horizontal", "attack1_phase3_horizontal"};

    public VisualEffect slashHorizontalVFX;

    public VisualEffect slashVerticalVFX;

    public VisualEffect dashVFX;
    
    int leftCurrentBasicAttackPhase = 0;

    int rightCurrentBasicAttackPhase = 0;

    float timeSinceLastBasicAttack = 0f;
    public float comboTiming = 2f;

    override public void Start() {
        base.Start();
        Scythe.SetActive(true);
        dashVFX.Stop();
        slashHorizontalVFX.Stop();
        slashVerticalVFX.Stop();
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
        slashHorizontalVFX.Play();

        var audioEvent = RuntimeManager.CreateInstance(scytheSlash);
        audioEvent.start();
        audioEvent.release();

        leftCurrentBasicAttackPhase = (leftCurrentBasicAttackPhase + 1) % horizontalBasicAttackAnimationNames.Length;
        timeSinceLastBasicAttack = Time.time;
    }

    public override void ExecuteRightBasicAttack() {
        if (Time.time - timeSinceLastBasicAttack > comboTiming) {
            rightCurrentBasicAttackPhase = 0;
        }

        m_Animator.SetTrigger(verticalBasicAttackAnimationNames[rightCurrentBasicAttackPhase]);
        slashVerticalVFX.Play();

        var audioEvent = RuntimeManager.CreateInstance(scytheSlash);
        audioEvent.start();
        audioEvent.release();

        rightCurrentBasicAttackPhase = (rightCurrentBasicAttackPhase + 1) % verticalBasicAttackAnimationNames.Length;
        timeSinceLastBasicAttack = Time.time;
    }

    public override void ExecuteAbility1() {
        m_Animator.SetTrigger("dash");
        dashVFX.Play();
        dashHitbox.SetActive(true);

        new WaitForSeconds(0.2f);
        Vector3 moveDir = playerController.getCharacterFacingDirection();
        playerController.MovePlayer(moveDir.normalized * 30f * (1.5f / 3));
        new WaitForSeconds(0.2f);
        dashHitbox.SetActive(false);

        SetNextAbility1Time();
    }

    public override void ExecuteAbility2() {
        if(playerController.IsGrounded()){
            playerController.Jump();
            ExecuteRightBasicAttack();
        }
        else{
            m_Animator.SetTrigger("plungeAttack");
            playerController.Dive();

            Collider[] hitColliders = scytheExplosion.ExplosionDamage(playerController.GetCharacterGlobalPosition());

            TriggerAOEDamage(hitColliders, scytheExplosion.damage);
            SetNextAbility2Time();
        }
        
    }

    private void TriggerAOEDamage(Collider[] hitColliders, float aoeDamage){
        
        foreach (var hitCollider in hitColliders)
        {
           CharacterModel model = hitCollider.gameObject.GetComponent<CharacterModel>();
            if (model != null && hitCollider.gameObject.tag != "Player") {
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
