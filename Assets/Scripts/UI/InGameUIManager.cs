using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InGameUIManager : MonoBehaviour
{
    public Button button_LoadStage;
    public Button button_EditorArea;

    public Button button_Go;
    public Button button_Right;
    public Button button_Left;

    public Button button_Reset;
    private Button button_Edit;

    public GameObject gameObject_Button_Edit;
    public GameObject gameObject_Button_BackToGameMode;

    public Button button_StageSave;
    public Button button_StageCancel;

    public Text text_PinNum;

    void Awake()
    {
        button_Edit = gameObject_Button_Edit.GetComponent<Button>();
    }

    public void Start()
    {
        OnSetUp();
    }

    public int Text_PinNum
    {
        set{ text_PinNum.text = ""+value; }
    }

    public void OnSetUp()
    {
        button_EditorArea.enabled = false;
        button_Go.interactable = true;
        button_Right.interactable = true;
        button_Left.interactable = true;

        gameObject_Button_Edit.SetActive(true);
        button_Edit.interactable = true;

        button_LoadStage.interactable = true;
        button_Reset.interactable = false;

        gameObject_Button_BackToGameMode.SetActive(false);

    }

    public void OnEdit()
    {
        button_EditorArea.enabled = true;
        button_Go.interactable = false;
        button_Right.interactable = false;
        button_Left.interactable = false;

        button_Reset.interactable = false;

        gameObject_Button_Edit.SetActive(false);
        gameObject_Button_BackToGameMode.SetActive(true);

    }

    public void OnThrowing()
    {
        button_Edit.interactable = false;
        button_LoadStage.interactable = false;
        button_Reset.interactable = true;
    }
}
