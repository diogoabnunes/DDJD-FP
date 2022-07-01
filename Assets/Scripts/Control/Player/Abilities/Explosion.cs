using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using FMODUnity;

public class Explosion : MonoBehaviour
{
    public float damage;
    public float radius;

    public VisualEffect explosionVFX;

    [EventRef, SerializeField] string explosionSFX = default;

    public void Start(){
        explosionVFX.Stop();
    }
    public Collider[] ExplosionDamage(Vector3 detonationLocation)
    {   
        detonationLocation = new Vector3(detonationLocation.x, -6.54f, detonationLocation.z);
        Collider[] hitColliders = Physics.OverlapSphere(detonationLocation, radius);
        
        var audioEvent = RuntimeManager.CreateInstance(explosionSFX);
        audioEvent.start();
        audioEvent.release();

        explosionVFX.Play();
        return hitColliders;
    }
}