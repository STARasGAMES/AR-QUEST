using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARQuestCreator
{
    public class UIPushNotificationController : MonoBehaviour, IUIController
    {
        [SerializeField] Text _textMessage;
        [SerializeField] Image _imageNotificationColor;
        public enum NotificationType
        {
            Positive,
            Neutral,
            Negative,
            Help
        }

        public enum NotificationLifeTimeType
        {
            Long,
            Medium,
            Short
        }

        [Header("Notification type colors")]
        [SerializeField] Color _positive = Color.green;
        [SerializeField] Color _neutral = Color.gray;
        [SerializeField]  Color _negative = Color.red;
        [SerializeField] Color _help = Color.yellow;

        [Header("Notification life times")]
        [SerializeField] float _long = 10;
        [SerializeField] float _medium = 5;
        [SerializeField] float _short = 3;


        private Animator _animator;

        public void Hide()
        {
            Debug.Log("Hide Notification");
            _animator.SetBool("Show", false);
        }

        public void Show()
        {
            Debug.Log("Show Notification");
            _animator.SetBool("Show", true);
        }

        // Use this for initialization
        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetNotification(string message,NotificationLifeTimeType notificationLifeTime, NotificationType notificationType)
        {
            _textMessage.text = message;
            _imageNotificationColor.color = GetNotificationColor(notificationType);
            StopAllCoroutines();
            StartCoroutine(HideNotification(GetNotificationLifeTime(notificationLifeTime)));
        }

        private Color GetNotificationColor(NotificationType notificationType)
        {
            Color notificationColor = Color.black;
            switch (notificationType)
            {
                case NotificationType.Help:
                    notificationColor = _help;
                    break;
                case NotificationType.Negative:
                    notificationColor = _negative;
                    break;
                case NotificationType.Neutral:
                    notificationColor = _neutral;
                    break;
                case NotificationType.Positive:
                    notificationColor = _positive;
                    break;
                default:
                    Debug.LogError("Unknow notification type!");
                    Debug.Break();
                    break;
            }
            return notificationColor;
        }

        private float GetNotificationLifeTime(NotificationLifeTimeType type)
        {
            switch (type)
            {
                case NotificationLifeTimeType.Long:
                    return _long;
                case NotificationLifeTimeType.Medium:
                    return _medium;
                case NotificationLifeTimeType.Short:
                    return _short;
            }
            Debug.LogError("Unknow notificationLifeTime!");
            Debug.Break();
            return 10;
        }

        IEnumerator HideNotification(float notificationLifeTime)
        {
            yield return new WaitForSeconds(notificationLifeTime);
            Hide();
        }
    }
}
