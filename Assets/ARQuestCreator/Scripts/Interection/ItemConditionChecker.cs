using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQuestCreator
{
    public class ItemConditionChecker : MonoBehaviour, IConditionChecker
    {

        [SerializeField] List<Item> _requiredItems = new List<Item>();

        public bool IsSatisfied()
        {
            return PlayerInventory.Instance.ContainsItems(_requiredItems);
        }

    }
}