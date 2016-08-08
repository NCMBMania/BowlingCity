using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UI
{
    public class MailFieldPresenter : MonoBehaviour
    {
        private Image thisImage;
        private List<Text> fieldElements;
        public GameProgress.TitleStateManager titleStateManager;

        void Awake()
        {
            thisImage = GetComponent<Image>();
            fieldElements = GetComponentsInChildren<Text>().ToList();
        }

        void Update()
        {
            bool isSignUpState = 
               titleStateManager.CurentTitleState == GameProgress.TitleStateManager.TitleState.SignUp;

            fieldElements.ForEach(text => text.enabled =isSignUpState);
            thisImage.enabled = isSignUpState;
        }
    }
}
