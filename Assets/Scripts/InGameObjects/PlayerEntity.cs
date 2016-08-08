using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace InGameObjects
{

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerEntity : MonoBehaviour, IInGameObject
    {
        private Transform thisTransform;
        private Rigidbody thisRidgidbody;
        private Vector3 defaultPos;
        private Quaternion defaultQ;

        public virtual void Awake()
        {
            thisTransform = this.transform;
            thisRidgidbody = GetComponent<Rigidbody>();

            defaultPos = this.transform.position;
            defaultQ = this.transform.rotation;

            InGameObjectsManager.Instance.AddInGameObjectList(this);
        }

        public void MoveForward(float speed)
        {
            thisRidgidbody.AddForce(thisTransform.forward * speed);
        }

        public void MoveHorizontal(float amount)
        {
            thisTransform.Translate(Vector3.right * amount);
        }

        public void ResetPosition()
        {
            thisTransform.position = defaultPos;
            thisTransform.rotation = defaultQ;

            thisRidgidbody.velocity = Vector3.zero;
            thisRidgidbody.angularVelocity = Vector3.zero;
        }

    }
}

