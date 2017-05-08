using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQuestCreator
{
    public class GameManager : Singleton<GameManager>{

        private void Start()
        {
            ScreenSpaceUIController.Instance.ShowUI(ScreenSpaceUIController.UIType.Player);
        }

        public void ViewItem(Item item)
        {
            //PlayerInventory.Instance.RemoveItem(item);
            ItemViewer.Instance.ViewItem(item);
            
            ScreenSpaceUIController.Instance.ShowUI(ScreenSpaceUIController.UIType.ItemView);
        }
                     
        public void PickupItem(Item item)
        {
            PlayerInventory.Instance.AddItem(item);
            ScreenSpaceUIController.Instance.ShowUI(ScreenSpaceUIController.UIType.Player);
        }

        public void OnInventoryShow()
        {
            ScreenSpaceUIController.Instance.ShowUI(ScreenSpaceUIController.UIType.Inventory);
        }

        public void OnInventoryHide()
        {
            ScreenSpaceUIController.Instance.ShowUI(ScreenSpaceUIController.UIType.Player);
        }

    }
}


