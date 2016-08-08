using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class TitleUIManager : MonoBehaviour
{
    public Text text_Title;
    public InputField inputFIeld_ID;
    public InputField inputFIeld_Pass;
    public InputField inputFIeld_Email;
    public GameObject gameObject_Conecting;
    public Text text_TryAgain;
    public Button button_StartAuth;
    private Text buttonText_LogIn;
    public Button button_ChangeMode;

    public void ShowLogInMenu(UnityAction action_StartLogInAuth, UnityAction action_ChangeMode)
    {
        text_Title.text = "Log In";
        button_StartAuth.SetNewButtonAction("Log In", action_StartLogInAuth);
        button_ChangeMode.SetNewButtonAction("Create New Account", action_ChangeMode);

        inputFIeld_Email.interactable = false;
    }

    public void ShowSignUpMenu(UnityAction action_StartSignUpAuth, UnityAction action_ChangeMode)
    {
        text_Title.text = "Sign Up";
        button_StartAuth.SetNewButtonAction("Sign Up", action_StartSignUpAuth);
        button_ChangeMode.SetNewButtonAction("Use Existed Account", action_ChangeMode);

        inputFIeld_Email.interactable = true;
    }

    public string InputFIeldText_ID
    {
        get{return inputFIeld_ID.text;}
    }

    public string InputFIeldText_Pass
    {
        get { return inputFIeld_Pass.text; }
    }

    public string InputFIeldText_Email
    {
        get { return inputFIeld_Email.text; }
    }


    public void ShowConnectingMessage()
    {
        gameObject_Conecting.SetActive(true);
    }

    public void CloseConnectingMessage()
    {
        gameObject_Conecting.SetActive(false);
    }

    public void ShowTryAgainText()
    {
        text_TryAgain.enabled = true;
    }

    public void CloseTryAgainText()
    {
        text_TryAgain.enabled = false;
    }

    public void SelectInputFieldPass()
    {
        inputFIeld_Pass.Select();
    }
}
