using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQuestCreator
{
    public class PlayerInventory : Singleton<PlayerInventory> {

        [SerializeField] Transform _inventoryRoot;
        //[SerializeField] List<Item> _items;

        public void AddItem(Item item)
        {
            item.transform.SetParent(_inventoryRoot);
            item.transform.localPosition = - Vector3.up * _inventoryRoot.childCount;
            item.ApplyCurrentParent();
            item.state = Item.ItemState.Inventory;
            item.enabled = false;
        }

        public Item[] GetItems()
        {
            return _inventoryRoot.GetComponentsInChildren<Item>(true);
        }

        public bool RemoveItem(Item item)
        {
            item.transform.parent = null;
            return true;
        }
    }
}

