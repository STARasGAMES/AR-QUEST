using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ARQuestCreator.UI;

namespace ARQuestCreator.MiniGames.TubeGame
{
    [ExecuteInEditMode]
    public class TubeGameManager : MonoBehaviour, IMiniGameManager
    {
        public int rowCount = 5;
        public int columnCount = 5;
        public float waterSpeed = 5;
        private BaseTubeController[,] _tubes;
        private StartTubeController _startTubeCntrl;
        private List<EndTubeController> _endTubesCntrl;


        public bool isGamePassed { get; private set; }

        void Awake()
        {
            LoadGame();
        }

        public void LoadGame()
        {
            _tubes = new BaseTubeController[columnCount, rowCount];
            _startTubeCntrl = GetComponentInChildren<StartTubeController>();
            _endTubesCntrl = new List<EndTubeController>(GetComponentsInChildren<EndTubeController>());
            foreach (var tube in GetComponentsInChildren<BaseTubeController>())
            {
                tube.Init();
            }
        }

        private void OnEnable()
        {
            foreach (var end in _endTubesCntrl)
            {
                end.onFilledEventHandler += OnEndTubeFilled;
            }
        }

        private void OnDisable()
        {
            foreach (var end in _endTubesCntrl)
            {
                end.onFilledEventHandler -= OnEndTubeFilled;
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            RectTransform rt = GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(columnCount, rowCount);
#endif
            if (isGamePassed)
                return;
            _startTubeCntrl.AddFill(waterSpeed * Time.deltaTime);
        }

        public void AddTube(BaseTubeController tube)
        {
            int x = (int)Mathf.Round(tube.position.x);
            int y = (int)Mathf.Round(tube.position.y);
            _tubes[x,y] = tube;
        }

        public BaseTubeController GetTubeAtPosition(Vector2 position)
        {
            int x = (int)Mathf.Round(position.x);
            int y = (int)Mathf.Round(position.y);
            if (isInRange(x, y))
                return _tubes[x, y];
            return null;
        }

        private bool isInRange(int x, int y)
        {
            return x < _tubes.GetLength(0) && y < _tubes.GetLength(1) && x >= 0 && y >= 0;
        }

        public bool IsGamePassed()
        {
            foreach(var e in _endTubesCntrl)
            {
                if (!e.isFilled())
                {
                    isGamePassed = false;
                    return false;
                }
            }
            isGamePassed = true;
            return true;
        }

        void OnEndTubeFilled()
        {
            Debug.Log("OnEndTubeFilled");
            if (IsGamePassed())
            {
                //ScreenSpaceUIManager.Instance.ShowNotification("Game complited!", UIPushNotificationController.NotificationLifeTimeType.Short, UIPushNotificationController.NotificationType.Positive);
            }
            foreach(var tube in _tubes)
            {
                tube.enabled = !isGamePassed;
            }
        }
    }
}
