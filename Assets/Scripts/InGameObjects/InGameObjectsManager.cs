using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InGameObjects
{
    public class InGameObjectsManager : SingletonMonoBehaviour<InGameObjectsManager>
    {
        public GameObject gameObject_Player;
        public PlayerEntity playerEntity { get; private set; }

        public GameObject pinPrefab;
        public Camera editorCamera;

        public int maxPinNum = 20;

        public override void Awake()
        {
           base.Awake();

           playerEntity = gameObject_Player.GetComponent<PlayerEntity>();

           for(int i =0; i < maxPinNum; i++)
           {
               GameObject target = (GameObject)Instantiate(pinPrefab);
               target.transform.parent = this.gameObject.transform;
               target.GetComponent<BowlingPin>().RemoveFromStage();
           }

        }

        private List<IInGameObject> ingameObjectList = new List<IInGameObject>();
        private List<BowlingPin> bowlingPinList = new List<BowlingPin>();


        public void AddInGameObjectList(IInGameObject go)
        {
            ingameObjectList.Add(go);
        }

        public void ResetAllGameObjects()
        {
            //DisableAllPin();
            currentPinList.ForEach(pin => pin.ResetCurrentPosition());
            ingameObjectList.ForEach(st => st.ResetPosition());
        }


        public void AddPinStatusList(BowlingPin pin)
        {
            bowlingPinList.Add(pin);
        }

        public int ValidBowlingPinNum
        {
            get
            {
                return bowlingPinList
              .Where(pin => pin.IsEnabled == true)
              .Count(pin => pin.IsGrounded == false);
            }
        }

        List<BowlingPin> currentPinList
        {
            get
            {
                return bowlingPinList.Where(pin => pin.IsEnabled == true).ToList();
            }
        }

        public List<double[]> GetCurrentPinPositionByDoubleArrayList()
        {
            return currentPinList.Select(pin => Utility.Vector3toDoubleArray(pin.CurrentPos)).ToList();
        }

        public void DisableAllPin()
        {
            bowlingPinList.ForEach(pin => pin.RemoveFromStage());
        }

        public void PositioningPinAtMousePosition()
        {
            if (Input.GetMouseButtonUp(0) == true)
            {
                Vector3 mouseWorldPos = editorCamera.ScreenToWorldPoint(
                    new Vector3(
                        Input.mousePosition.x,
                        Input.mousePosition.y,
                        10f)
                    );

                PositioningPin(new Vector3(mouseWorldPos.x, 0f, mouseWorldPos.z));
            }

        }

        public bool PositioningPin(Vector3 pos)
        {
            BowlingPin pin = bowlingPinList.First(p => p.IsEnabled == false);

            if (pin != null)
            {
                pin.SetPosition(pos);
                return true;
            }

            return false;
           
        }
    }

}
