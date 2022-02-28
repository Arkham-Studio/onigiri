using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;

namespace Arkham.Onigiri.Physics3DModule
{
    public class RigidbodyToChangeableVariable : MonoBehaviour
    {

        [SerializeField] private Rigidbody myRigidbody;

        [SerializeField, EnumToggleButtons] private InfosType types;
        [SerializeField] private Vector3Variable velocity;

        private void OnValidate()
        {
            myRigidbody = myRigidbody == null ? GetComponent<Rigidbody>() : myRigidbody;
        }

        private void FixedUpdate()
        {
            velocity.SetValue(myRigidbody.velocity);
        }

        [System.Flags]
        enum InfosType { velocity, direction, magnitude, angularVelocity }
    }
}

