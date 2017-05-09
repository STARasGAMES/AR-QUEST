using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQuestCreator {
    public class ScreenSpaceUIManager : Singleton<ScreenSpaceUIManager> {

        [SerializeField] UIInventoryController _inventoryUI;
        [SerializeField] UIViewItemController _itemViewUI;
        [SerializeField] UIPlayerController _playerUI;
        [SerializeField] UIPushNotificationController _notificationUI;
        private IUIController[] _array;

        public enum UIType
        {
            Nothing,
            Inventory,
            ItemView,
            Player
        }

        private void Awake()
        {
            _array = new IUIController[] {
                _inventoryUI,
                _itemViewUI,
                _playerUI
            };
        }

        public void ShowUI(UIType uitype)
        {
            int index = -1;
            switch (uitype)
            {
                case UIType.Inventory:
                    index = 0;
                    break;
                case UIType.ItemView:
                    index = 1;
                    break;
                case UIType.Player:
                    index = 2;
                    break;
                default:
                    Debug.LogError("Undefined UIType!!!", this);
                    return;
                    break;
            }

            for (int i=0; i<_array.Length; i++)
            {
                if (i == index)
                {
                    _array[i].Show();
                }
                else
                {
                    _array[i].Hide();
                }
            }
        }

        public void ShowNotification(string message,
            UIPushNotificationController.NotificationLifeTimeType lifeTime = UIPushNotificationController.NotificationLifeTimeType.Medium,
            UIPushNotificationController.NotificationType type = UIPushNotificationController.NotificationType.Neutral)
        {
            _notificationUI.SetNotification(message, lifeTime, type);
            _notificationUI.Show();
        }
    }
}
