using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using InGameObjects;

namespace GameProgress
{
    public enum GameState { SetUp, Throwing, Score,Edit }

    //ゲーム全体の進行を管理するクラス//
    public class MainGameStateManager : SingletonMonoBehaviour<MainGameStateManager>
    {
        public Camera playerCamera;
        public Camera editCamera;

        public InGameUIManager inGameUIManager;

        [SerializeField]
        private GameState currentGameState = GameState.SetUp;
        public GameState CurrentGameState
        {
            get { return currentGameState; }
        }

        public bool IsGameState(GameState gameState)
        {
            return (currentGameState == gameState);
        }

        public void Start()
        {
            OnSetUp();
        }

        public void Update()
        {
            switch (currentGameState)
            {
                case GameState.SetUp:
                    break;
                case GameState.Throwing:
                    SetUIByArrivePinNum();
                    break;
                case GameState.Score:
                    break;
                case GameState.Edit:
                    break;
                default:
                    break;
            }
        }

        void SetUIByArrivePinNum()
        {
            inGameUIManager.Text_PinNum = InGameObjectsManager.Instance.ValidBowlingPinNum;
        }

        public void OnSetUp()
        {
            currentGameState = GameState.SetUp;
            playerCamera.enabled = true;
            editCamera.enabled = false;

            inGameUIManager.OnSetUp();

            SetUIByArrivePinNum();
        }

        public void OnThrowing()
        {
            currentGameState = GameState.Throwing;

            inGameUIManager.OnThrowing();
        }

        public void OnScore()
        {
            currentGameState = GameState.Score;
        }

        public void OnEdit()
        {
            currentGameState = GameState.Edit;
            playerCamera.enabled = false;
            editCamera.enabled = true;

            inGameUIManager.Text_PinNum = 0;

            inGameUIManager.OnEdit();
        }

    }

}