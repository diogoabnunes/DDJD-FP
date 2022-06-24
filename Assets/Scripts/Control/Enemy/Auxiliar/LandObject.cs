using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LandObject : MonoBehaviour
{
    public EnemyController controller;
    public EnemyModel model;

    Rigidbody rigidbody;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        if (CollidedWithGround()) {
            EnableControlls();
        }
    }

    bool CollidedWithGround() {
        return rigidbody.velocity.y == 0;
    }

    void EnableControlls() {
        rigidbody.isKinematic = true;
        rigidbody.freezeRotation = false;

        GetComponent<NavMeshAgent>().enabled = true;

        controller.enabled = true;
        model.enabled = true;

        this.enabled = false;
    }
}
