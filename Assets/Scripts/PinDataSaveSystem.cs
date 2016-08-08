using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NCMB;
using System;

namespace GameProgress
{
    public class PinDataSaveSystem : SingletonMonoBehaviour<PinDataSaveSystem>
    {
        public InGameObjects.InGameObjectsManager inGameObjectsManager;


        public void Save()
        {
            string userName = UserAuth.Instance.CurrentPlayerName;
            userName = string.IsNullOrEmpty(userName) ? "Default" : userName;

            List<double[]> pinPositionList = inGameObjectsManager.GetCurrentPinPositionByDoubleArrayList();
            if(pinPositionList.Count == 0)
            {
                Debug.LogWarning("no pin position data");
            }
            else
            {
                SaveToNCMB(userName, pinPositionList);
            }
        }

        void SaveToNCMB(string userName, List<double[]> pinPositionList)
        {
            NCMBObject ncmbObject_PinPosition = new NCMBObject("PinPosition");

            ncmbObject_PinPosition.AddRangeToList("PinPositionList", pinPositionList);
            ncmbObject_PinPosition.Add("UserName", userName);

            ncmbObject_PinPosition.SaveAsync((NCMBException e) =>
            {
                if (e != null)
                {
                    //エラー処理
                }
            });
        }

        public void Load()
        {
            inGameObjectsManager.DisableAllPin();
            LoadNewestFromNCMB();
        }

        void LoadNewestFromNCMB()
        {
            //PinPositionクラスを検索するクエリを作成
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("PinPosition");

            //日付順にソート//
            query.OrderByDescending("createDate");

            //先頭の要素だけを取得する
            query.Limit = 1;

            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                if (e != null)
                {
                    //データは見つからなかった
                }
                else
                {
                    ArrayList arrayList = (ArrayList)objList[0]["PinPositionList"];

                    foreach(ArrayList a in arrayList)
                    {
                        inGameObjectsManager.PositioningPin(Utility.DoubleArrayListToVector3(a));
                    }
                }
            });
        }
 
    }
}
