using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private InteractionManager interactionManager;

    private GameManager gameManager;

    public float damage = 1.0f;
    public float speed = 1.0f;

    Rigidbody rigidBody;

    Vector3 target;
    GameObject launcher;

    bool active = true;

    void Awake() {
        interactionManager = InteractionManager.instance;
        gameManager = GameManager.instance;

        rigidBody = GetComponent<Rigidbody>();
    }

    void Start() {

    }

    void Update() {

    }

    public void SetTarget(Vector3 _target) {
        target = _target;
        Fire();
    }

    public void SetLauncher(GameObject _launcher) {
        launcher = _launcher;
    }

    void Fire() {
      Vector3 distance = target - transform.position;
      rigidBody.velocity = Vector3.Normalize(distance) * speed;
    }

    private void OnTriggerEnter(Collider other) {
        CharacterModel model = null;

        if (!active || CollidedWithObjectLauncher(other)) return;

        if (other.gameObject.tag == "PlayerWeapon")
            return;

        if (other.gameObject.tag == "Player") {
            model = gameManager.GetComponent<CharacterModel>();
            active = false;
        }

        if (model != null) {
            interactionManager.manageInteraction(new TakeDamage(damage, model));
        }

        Debug.Log(other.gameObject.name);
        Destroy(gameObject);
    }

    bool CollidedWithObjectLauncher(Collider other) {
        return other.gameObject == launcher;   
    }
}
