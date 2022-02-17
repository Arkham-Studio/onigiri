using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arkham.Onigiri.Variables;

namespace Arkham.Onigiri.Utils
{
    public class TransformExtend : MonoBehaviour
    {
        [SerializeField] private Transform myTransform;

        private void OnEnable()
        {
            if (myTransform == null) myTransform = transform;
        }

        //POSITION
        public void SetPosition(Vector3Variable _v) => myTransform.position = _v.Value;
        public void SetPositionX(float _x) => myTransform.position = new Vector3(_x, myTransform.position.y, myTransform.position.z);
        public void SetPositionY(float _y) => myTransform.position = new Vector3(myTransform.position.x,_y, myTransform.position.z);
        public void SetPositionZ(float _z) => myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y, _z);
        public void SetPositionX(FloatVariable _v) => myTransform.position = new Vector3(_v.Value, myTransform.position.y, myTransform.position.z);
        public void SetPositionY(FloatVariable _v) => myTransform.position = new Vector3(myTransform.position.x, _v.Value, myTransform.position.z);
        public void SetPositionZ(FloatVariable _v) => myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y, _v.Value);

        //ROTATION
        public void SetRotation(Vector3Variable _v) => myTransform.rotation = Quaternion.Euler(_v.Value);
        public void SetRotationX(float _x) => myTransform.rotation = Quaternion.Euler(_x, myTransform.rotation.eulerAngles.y, myTransform.rotation.eulerAngles.z);
        public void SetRotationY(float _y) => myTransform.rotation = Quaternion.Euler(myTransform.rotation.eulerAngles.x, _y, myTransform.rotation.eulerAngles.z);
        public void SetRotationZ(float _z) => myTransform.rotation = Quaternion.Euler(myTransform.rotation.eulerAngles.x, myTransform.rotation.eulerAngles.y, _z);
        public void SetRotationX(FloatVariable _v) => myTransform.rotation = Quaternion.Euler(_v.Value, myTransform.rotation.eulerAngles.y, myTransform.rotation.eulerAngles.z);
        public void SetRotationY(FloatVariable _v) => myTransform.rotation = Quaternion.Euler(myTransform.rotation.eulerAngles.x, _v.Value, myTransform.rotation.eulerAngles.z);
        public void SetRotationZ(FloatVariable _v) => myTransform.rotation = Quaternion.Euler(myTransform.rotation.eulerAngles.x, myTransform.rotation.eulerAngles.y, _v.Value);

        //SCALE
        public void SetScale(Vector3Variable _v) => myTransform.localScale = _v.Value;
        public void SetScaleX(float _x) => myTransform.localScale = new Vector3(_x, myTransform.localScale.y, myTransform.localScale.z);
        public void SetScaleY(float _y) => myTransform.localScale = new Vector3(myTransform.localScale.x, _y, myTransform.localScale.z);
        public void SetScaleZ(float _z) => myTransform.localScale = new Vector3(myTransform.localScale.x, myTransform.localScale.y, _z);
        public void SetScaleX(FloatVariable _v) => myTransform.localScale = new Vector3(_v.Value, myTransform.localScale.y, myTransform.localScale.z);
        public void SetScaleY(FloatVariable _v) => myTransform.localScale = new Vector3(myTransform.localScale.x, _v.Value, myTransform.localScale.z);
        public void SetScaleZ(FloatVariable _v) => myTransform.localScale = new Vector3(myTransform.localScale.x, myTransform.localScale.y, _v.Value);



    }

}