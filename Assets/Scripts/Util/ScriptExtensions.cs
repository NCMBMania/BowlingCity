using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public static class ScriptExtensions{

    public static void SetNewButtonAction(this Button button, string text, UnityAction action)
    {
        button.GetComponentInChildren<Text>().text = text;
        SetNewButtonAction(button, action);
    }


    public static void SetNewButtonAction(this Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
        button.enabled = true;
    }

}
