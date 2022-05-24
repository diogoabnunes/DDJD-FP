using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToAction : Action
{
    NavMeshAgent agent;
    Vector3 destination;

    public MoveToAction(NavMeshAgent agent, Vector3 destination) {
        this.agent = agent;
        this.destination = destination;
    }

    public override void execute() {
        agent.SetDestination(destination);
    }
}
