using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ARQuestCreator
{
    public class UIInventoryController : MonoBehaviour, IUIController
    {

        public RectTransform grid;
        [SerializeField] UIItemController _uiItemPrefab;
        private Animator _animator;

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
            UpdateGrid();
        }

        public void UpdateGrid()
        {
            Item[] items = PlayerInventory.Instance.GetItems();
            int x;
            if ((x = items.Length - grid.childCount) > 0)
            {
                for (; x > 0; x--)
                {
                    GameObject go = ((UIItemController)Instantiate(_uiItemPrefab)).gameObject;
                    go.transform.SetParent(grid);
                    go.transform.localScale = Vector3.one;
                }
            }
            UIItemController[] uiItems = grid.GetComponentsInChildren<UIItemController>(true);
            for (int i=0; i<uiItems.Length; i++)
            {
                if (i >= items.Length)
                    uiItems[i].Hide();
                else
                {
                    uiItems[i].SetItem(items[i]);
                    uiItems[i].Show();
                }
            }
        }
    }
}