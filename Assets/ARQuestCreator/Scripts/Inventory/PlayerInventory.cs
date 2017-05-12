using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQuestCreator
{
    public class PlayerInventory : Singleton<PlayerInventory> {

        [SerializeField] Transform _inventoryRoot;
        [SerializeField] List<Item> _items;

        private void Start()
        {
            
        }

        public void AddItem(Item item)
        {
            if (!ContainsItem(item))
                _items.Add(item);
            else
                Debug.LogError("WTF", item);
            item.transform.SetParent(_inventoryRoot);
            item.transform.localPosition = - Vector3.up * _inventoryRoot.childCount;
            item.state = Item.ItemState.Inventory;
            item.SetAtcive(false);
            item.ApplyCurrentParent();
        }

       public Item[] GetItems()
        {
            return _items.ToArray();
        }

        public bool RemoveItem(Item item)
        {
            item.transform.parent = null;
            return true;
        }

        public bool ContainsItem(Item item)
        {
            return _items.Contains(item);
        }

        public bool ContainsItems(List<Item> items)
        {
            foreach(var item in items)
            {
                if (!ContainsItem(item))
                    return false;
            }
            return true;
        }
    }
}

