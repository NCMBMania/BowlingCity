using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace GameProgress
{
    public class TitleStateManager : MonoBehaviour
    {
        UnityEngine.UI.Text title;

        public enum TitleState {Advertize, Title, Login, SignUp }

        public TitleUIManager titleUIManager;

        [SerializeField]
        public TitleState CurentTitleState
        {
            get;
            private set;
        }

        // Use this for initialization
        void Start()
        {
            OnLogIn();
            titleUIManager.CloseTryAgainText();
        }

        public void OnLogIn()
        {
            CurentTitleState = TitleState.Login;

            titleUIManager.CloseConnectingMessage();

            titleUIManager.ShowLogInMenu(
                action_StartLogInAuth: OnLogIngUserAuth,
                action_ChangeMode: OnSignUp
                );
        }

        public void OnSignUp()
        {
            CurentTitleState = TitleState.SignUp;

            titleUIManager.CloseConnectingMessage();

            titleUIManager.ShowSignUpMenu(
                action_StartSignUpAuth: OnSignUPUserAuth,
                action_ChangeMode: OnLogIn
                );
        }

        public void OnLogIngUserAuth()
        {
            UserAuth.Instance.LogIn(
                titleUIManager.InputFIeldText_ID,
                titleUIManager.InputFIeldText_Pass,
                callback_Success: OnLoadCoreScene,
                callback_Failed: OnTryAgain
                );

            titleUIManager.ShowConnectingMessage();
        }

        public void OnSignUPUserAuth()
        {
            UserAuth.Instance.SignUp(
                titleUIManager.InputFIeldText_ID,
                titleUIManager.InputFIeldText_Email,
                titleUIManager.InputFIeldText_Pass,
                callback_Success: OnLoadCoreScene,
                callback_Failed: OnTryAgain
                );

            titleUIManager.ShowConnectingMessage();
        }

        public void OnLoadCoreScene()
        {
            SceneManager.LoadScene("Core");
        }

        public void OnTryAgain()
        {
            titleUIManager.CloseConnectingMessage();
            titleUIManager.ShowTryAgainText();
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                titleUIManager.SelectInputFieldPass();
            }

        }

    }
}


