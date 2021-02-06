using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemExtend : MonoBehaviour
{
    [Title("REFERENCES")]
    [SerializeField] private ParticleSystem myParticleSystem;

    [Title("PHYSICS", "collisions")]
    [SerializeField] private ParticleCollisionEventVariable actualCollisionEvent;
    private List<ParticleCollisionEvent> particleCollisionEvents = new List<ParticleCollisionEvent>();

    [Title("", "triggers")]
    [SerializeField] private ParticleVariable actualParticleTriggerEnter;
    [SerializeField] private ParticleVariable actualParticleTriggerExit;
    private List<ParticleSystem.Particle> enterTriggerParticles = new List<ParticleSystem.Particle>();
    private List<ParticleSystem.Particle> exitTriggerParticles = new List<ParticleSystem.Particle>();



    //  MONO
    void OnEnable()
    {
        myParticleSystem = myParticleSystem ?? GetComponent<ParticleSystem>();
    }


    //  UTILS
    public void ChangeEmisionRateOverTime(MinMaxCurveVariable _minMaxVariable)
    {
        var _emission = myParticleSystem.emission;
        _emission.rateOverTime = _minMaxVariable.Value;
    }


    //  PHYSICS
    private void OnParticleCollision(GameObject _other)
    {
        if (!myParticleSystem.collision.enabled && !myParticleSystem.collision.sendCollisionMessages) return;

        int _numCollisionEvents = myParticleSystem.GetCollisionEvents(_other, particleCollisionEvents);
        foreach (ParticleCollisionEvent item in particleCollisionEvents)
            actualCollisionEvent.SetValue(item);
    }

    private void OnParticleTrigger()
    {
        if (!myParticleSystem.trigger.enabled) return;

        int _numEnter = myParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterTriggerParticles);
        int _numExit = myParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exitTriggerParticles);

        int _max = Mathf.Max(_numEnter, _numExit);
        for (int i = 0; i < _max; i++)
        {
            if (i < _numEnter)
                actualParticleTriggerEnter.SetValue(enterTriggerParticles[i]);
            if (i < _numExit)
                actualParticleTriggerExit.SetValue(exitTriggerParticles[i]);
        }

    }
}
