using Arkham.Onigiri.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkham.Onigiri.TransformModule
{
    public class VectorVariableToTransform : MonoBehaviour
    {
        [SerializeField] private Transform myTransform;
        [SerializeField] private ChangeableVariable positionVariable;
        [SerializeField] private ChangeableVariable rotationVariable;
        [SerializeField] private ChangeableVariable scaleVariable;

        private void OnEnable()
        {
            positionVariable?.onChange.AddListener(UpdatePosition);
            rotationVariable?.onChange.AddListener(UpdateRotation);
            scaleVariable?.onChange.AddListener(UpdateScale);
        }

        private void OnDisable()
        {
            positionVariable?.onChange.RemoveListener(UpdatePosition);
            rotationVariable?.onChange.RemoveListener(UpdateRotation);
            scaleVariable?.onChange.RemoveListener(UpdateScale);
        }

        private void UpdatePosition()
        {
            switch (myTransform)
            {
                case RectTransform _rT:
                    switch (positionVariable)
                    {
                        case Vector2Variable _v2:
                            _rT.anchoredPosition = _v2.Value;
                            break;
                        case Vector3Variable _v3:
                            _rT.anchoredPosition = new Vector2(_v3.X, _v3.Z);
                            break;
                        case FloatVariable _f:
                            myTransform.localPosition = new Vector2(_f, _f);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    switch (positionVariable)
                    {
                        case Vector2Variable _v2:
                            myTransform.localPosition = new Vector3(_v2.X, 0, _v2.Y);
                            break;
                        case Vector3Variable _v3:
                            myTransform.localPosition = _v3;
                            break;
                        case FloatVariable _f:
                            myTransform.localPosition = new Vector3(_f, _f, _f);
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }

        private void UpdateRotation()
        {
            switch (rotationVariable)
            {
                case Vector3Variable _v3:
                    myTransform.rotation = Quaternion.Euler(_v3);
                    break;
                default:
                    break;
            }
        }

        private void UpdateScale()
        {

            switch (scaleVariable)
            {
                case FloatVariable _f:
                    myTransform.localScale = new Vector3(_f, _f, _f);
                    break;
                case Vector3Variable _v3:
                    myTransform.localScale = _v3;
                    break;
                default:
                    break;
            }

        }

    }
}
