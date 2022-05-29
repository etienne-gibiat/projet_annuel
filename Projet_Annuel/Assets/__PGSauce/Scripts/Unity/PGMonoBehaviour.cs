using PGSauce.Core.Extensions;
using PGSauce.Internal.Attributes;
using UnityEngine;

namespace PGSauce.Unity
{
    [Version(0,0,1)]
    public class PGMonoBehaviour : MonoBehaviour
    {
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }
        public Vector3 EulerAngles
        {
            get => transform.eulerAngles;
            set => transform.eulerAngles = value;
        }
        public Vector3 GlobalScale
        {
            get => transform.lossyScale;
            set => transform.SetGlobalScale(value);
        }
        
        public Vector3 LocalPosition
        {
            get => transform.localPosition;
            set => transform.localPosition = value;
        }
        public Quaternion LocalRotation
        {
            get => transform.localRotation;
            set => transform.localRotation = value;
        }
        public Vector3 LocalEulerAngles
        {
            get => transform.localEulerAngles;
            set => transform.localEulerAngles = value;
        }
        public Vector3 LocalScale
        {
            get => transform.localScale;
            set => transform.localScale = value;
        }

        public Vector3 Forward
        {
            get => transform.forward;
            set => transform.forward = value;
        }
        public Vector3 Up
        {
            get => transform.up;
            set => transform.up = value;
        }
        public Vector3 Right
        {
            get => transform.right;
            set => transform.right = value;
        }
    }
}