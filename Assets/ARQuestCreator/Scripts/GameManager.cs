using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQuestCreator
{
    public class GameManager : Singleton<GameManager>{

        private void Start()
        {
            ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Player);
        }

        public void ViewItem(Item item)
        {
            //PlayerInventory.Instance.RemoveItem(item);
            if (ItemViewer.Instance.IsEmpty())
            {
                ItemViewer.Instance.ViewItem(item);
                ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.ItemView);
            }
        }
                     
        public void PickupItem(Item item)
        {
            PlayerInventory.Instance.AddItem(item);
            ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Player);
        }

        public void OnInventoryShow()
        {
            ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Inventory);
        }

        public void OnInventoryHide()
        {
            ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Player);
        }

        

    }
}


