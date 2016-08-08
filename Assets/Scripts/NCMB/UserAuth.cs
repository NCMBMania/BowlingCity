using UnityEngine;
using System.Collections;
using NCMB;
using System.Collections.Generic;
using System;

public class UserAuth : SingletonMonoBehaviour<UserAuth>
{

    public string CurrentPlayerName { private set; get; }

    // mobile backendに接続してログイン ------------------------

    public void LogIn(string id, string pw, Action callback_Success, Action callback_Failed)
    {

        NCMBUser.LogInAsync(id, pw, (NCMBException e) =>
        {
            // 接続成功したら
            if (e == null)
            {
                CurrentPlayerName = id;
                callback_Success();
            }
            else
            {
                callback_Failed();
            }
        });
    }

    // mobile backendに接続して新規会員登録 ------------------------

    public void SignUp(string id, string mail, string pw, Action callback_Success, Action callback_Failed)
    {
        if(string.IsNullOrEmpty(id)     ||
           string.IsNullOrEmpty(mail)   ||
           string.IsNullOrEmpty(pw)
            )
        {
            //error//
            return;
        }
        else
        {
            NCMBUser user = new NCMBUser();
            user.UserName = id;
            user.Email = mail;
            user.Password = pw;
            user.SignUpAsync((NCMBException e) =>
            {
                if (e == null)
                {
                    Debug.Log("会員登録成功");
                    CurrentPlayerName = id;
                    callback_Success();
                }
                else
                {
                    Debug.Log("会員登録失敗");
                    callback_Failed();
                }
            });
        }
    }

    // mobile backendに接続してログアウト ------------------------

    public void LogOut()
    {

        NCMBUser.LogOutAsync((NCMBException e) =>
        {
            if (e == null)
            {
                CurrentPlayerName = null;
            }
        });
    }

}