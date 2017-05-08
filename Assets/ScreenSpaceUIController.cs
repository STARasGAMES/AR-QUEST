using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQuestCreator {
    public class ScreenSpaceUIController : Singleton<ScreenSpaceUIController> {

        [SerializeField] UIInventoryController _inventoryUI;
        [SerializeField] UIViewItemController _itemViewUI;
        [SerializeField] UIPlayerController _playerUI;

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
    }
}
