using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LandObject : MonoBehaviour
{
    public EnemyController controller;
    public EnemyModel model;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

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
        // 
        // return Mathf.Abs(rigidbody.velocity.y) <= 0.0001f;
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
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
