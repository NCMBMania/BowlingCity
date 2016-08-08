using UnityEngine;
using System.Collections;
using System;

namespace InGameObjects
{
    [RequireComponent(typeof (Collider))]
    public class OnTriggerEventSender : MonoBehaviour
    {
        /// <summary>
        /// ゲームオブジェクトにアタッチしたら自動でisTriggerをオンにする
        /// </summary>
        void Reset()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        public Action Callback;

        void OnTriggerEnter()
        {
            if(Callback != null)
            {
                Callback();
            }
        }
    }
}
