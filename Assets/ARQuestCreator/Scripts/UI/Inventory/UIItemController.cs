using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ARQuestCreator
{
    public class UIItemController : MonoBehaviour, IUIController
    {

        [SerializeField] Text _itemNameText;
        [SerializeField] Image _itemImage;
        [SerializeField] Item _item;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void SetItem(Item item)
        {
            _item = item;
            _itemNameText.text = item.name;
            gameObject.SetActive(true);
        }

        void OnClick()
        {
            GameManager.Instance.ViewItem(_item);
        }
    }
}