using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InGameObjects
{
    public class BowlingPin : MonoBehaviour
    {
        private Transform thisTransform;
        private Rigidbody thisRigidbody;
        public OnTriggerEventSender groundedSensor;

        public Vector3 CurrentPos { get; private set; }
        private Quaternion defaultQ;

        public bool IsGrounded {  get; private set; }

        public bool IsEnabled
        {
            get { return gameObject.activeSelf; }
            private set { gameObject.SetActive(value); }
        }

        void Awake()
        {
            thisTransform = this.transform;
            defaultQ = thisTransform.rotation;
            thisRigidbody = GetComponent<Rigidbody>();

            groundedSensor.Callback = () => IsGrounded = true;

            InGameObjectsManager.Instance.AddPinStatusList(this);
            RemoveFromStage();
         }

        public void SetPosition(Vector3 pos)
        {
            CurrentPos = pos;
            ResetCurrentPosition();
        }

        public void RemoveFromStage()
        {
            IsEnabled = false;
            IsGrounded = false;

            thisRigidbody.velocity = Vector3.zero;
            thisRigidbody.angularVelocity = Vector3.zero;
        }

        public void ResetCurrentPosition()
        {
            IsEnabled = true;
            IsGrounded = false;

            thisTransform.position = CurrentPos;
            thisTransform.rotation = defaultQ;

            thisRigidbody.velocity = Vector3.zero;
            thisRigidbody.angularVelocity = Vector3.zero;
        }

    }
}
