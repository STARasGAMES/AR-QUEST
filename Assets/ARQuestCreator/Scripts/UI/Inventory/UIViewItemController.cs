using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARQuestCreator
{
    public class UIViewItemController : MonoBehaviour, IUIController
    {

        private Animator _animator;
        [SerializeField] Text _textDescription;
        [SerializeField] Button _btnDescription;
        [SerializeField] Button _btnPickup;
        [SerializeField] Button _btnLeave;
        [SerializeField] Button _btnBack;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
        }

        public void Hide()
        {
            _animator.SetBool("Showing", false);
        }

        public void Show()
        {
            _animator.SetBool("Showing", true);
            if (ItemViewer.Instance.currentItem == null)
            {
                Debug.LogError("WTF", this);
                Debug.Break();
                return;
            }
            SetItem(ItemViewer.Instance.currentItem);
        }

        void SetItem(Item item)
        {
            switch (item.state)
            {
                case Item.ItemState.Inventory:
                    _btnDescription.gameObject.SetActive(true);
                    _btnPickup.gameObject.SetActive(false);
                    _btnLeave.gameObject.SetActive(false);
                    _btnBack.gameObject.SetActive(true);
                    break;
                case Item.ItemState.World:
                    _btnDescription.gameObject.SetActive(true);
                    _btnPickup.gameObject.SetActive(true);
                    _btnLeave.gameObject.SetActive(true);
                    _btnBack.gameObject.SetActive(false);
                    break;
                default:
                    Debug.LogError("Unknow item state");
                    Debug.Break();
                    break;
            }
            _textDescription.text = item.description;
        }

        public void OnPickupItem()
        {
            Debug.Log("OnPickUpItem");
            Item currentItem = ItemViewer.Instance.GetBackItem();
            GameManager.Instance.PickupItem(currentItem);
            
        }

        public void OnLeaveItem()
        {
            Debug.Log("OnLeaveItem");
            Item currentItem = ItemViewer.Instance.GetBackItem();
            currentItem.GoToApplyedParent();
            switch (currentItem.state)
            {
                case Item.ItemState.Inventory:
                    currentItem.button.enabled = false;
                    ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Inventory);
                    break;
                case Item.ItemState.World:
                    currentItem.button.enabled = true;
                    ScreenSpaceUIManager.Instance.ShowUI(ScreenSpaceUIManager.UIType.Player);
                    break;
                default:
                    Debug.LogError("Unknow item state");
                    Debug.Break();
                    break;
            }
            currentItem = null;
        }
    }
}