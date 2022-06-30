using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage;
    public float radius;

    public Collider[] ExplosionDamage(Vector3 detonationLocation)
    {   
        detonationLocation = new Vector3(detonationLocation.x, -6.54f, detonationLocation.z);
        Debug.Log("Location");
        Debug.Log(detonationLocation);
        Debug.Log("Radius " + radius);
        Collider[] hitColliders = Physics.OverlapSphere(detonationLocation, radius);
        
        return hitColliders;
    }
}