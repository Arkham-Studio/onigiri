using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnigiriParticleModuleTest : MonoBehaviour
{
    [SerializeField] private ParticleCollisionEventVariable actualCollisionEvent;
    [SerializeField] private ParticleVariable enterParticle;
    [SerializeField] private ParticleVariable exitParticle;

    private void OnEnable()
    {
        actualCollisionEvent.onChange.AddListener(OnParticleCollide);

        enterParticle.onChange.AddListener(OnParticleTriggerEnter);
        exitParticle.onChange.AddListener(OnParticleTriggerExit);
    }

    private void OnDisable()
    {
        actualCollisionEvent.onChange.RemoveListener(OnParticleCollide);

        enterParticle.onChange.RemoveListener(OnParticleTriggerEnter);
        exitParticle.onChange.RemoveListener(OnParticleTriggerExit);
    }

    private void OnParticleTriggerExit()
    {
        Debug.Log("exit trigger =>" + exitParticle.Value.velocity);
    }

    private void OnParticleTriggerEnter()
    {
        Debug.Log("enter trigger =>" + enterParticle.Value.velocity);
    }

    private void OnParticleCollide()
    {
        Debug.Log("collide particle with =>" + actualCollisionEvent.Value.colliderComponent.transform.root.name);
    }
}
