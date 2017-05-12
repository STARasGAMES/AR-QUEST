using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARQuestCreator.UI;

namespace ARQuestCreator
{
    [RequireComponent(typeof(Animator))]
    public class ChestController : MonoBehaviour, IWorldButtonClickHandler
    {

        public WorldButton openButton;
        [SerializeField]
        bool _lockIsClosed = true;
        [SerializeField]
        bool _isOpened = false;
        [Header("Animator properties")]
        [SerializeField] string _isOpenParamName = "IsOpen";
        [Header("Notification strings")]
        [SerializeField]
        string _onLockClosed = "Closed lock! Find the key";
        [SerializeField]
        string _onLockOpen = "You opened the lock!";
        private Animator _anim;
        List<IConditionChecker> _requiredConditions;

        public void OnWorldButtonClickHandler()
        {
            Debug.Log("OnWorldButtonClickHandler", this);
            if (_lockIsClosed)
            {
                if (!IsAllRequiredConditionsSatisfied())
                {
                    ScreenSpaceUIManager.Instance.ShowNotification(_onLockClosed, UIPushNotificationController.NotificationLifeTimeType.Medium, UIPushNotificationController.NotificationType.Negative);
                    return;
                }
                _lockIsClosed = false;
                ScreenSpaceUIManager.Instance.ShowNotification(_onLockOpen, UIPushNotificationController.NotificationLifeTimeType.Short, UIPushNotificationController.NotificationType.Positive);
            }
            _isOpened = !_isOpened;
            _anim.SetBool(_isOpenParamName, _isOpened);
            
        }

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _lockIsClosed = true;
            _isOpened = false;
            _anim.SetBool(_isOpenParamName, _isOpened);
        }

        private void OnEnable()
        {
            openButton.SubscribeOnClickHandler(this);
        }

        private void OnDisable()
        {
            openButton.UnsubscribeOnClickHandler(this);
        }

        private bool IsAllRequiredConditionsSatisfied()
        {
            IConditionChecker [] _requiredConditions = GetComponents<IConditionChecker>();
            foreach(var condition in _requiredConditions)
            {
                if (!condition.IsSatisfied())
                    return false;
            }
            return true;
        }
    }
}
