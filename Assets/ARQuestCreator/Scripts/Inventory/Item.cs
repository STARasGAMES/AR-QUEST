using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace ARQuestCreator
{
    [RequireComponent(typeof(WorldButton))]
    [RequireComponent(typeof(BoxCollider))]
    public class Item : MonoBehaviour, IWorldButtonClickHandler
    {
        public string name = "defaultItemName";
        [TextArea(3, 20)]
        public string description = "default description";
        public bool pickable = true;
        public bool canBeViewed = true;
        public bool rotatable = true;
        public bool scalable = true;

        private WorldButton _btn;
        private Collider _collider;

#region Unity MonoBehaviur Events
        private void OnEnable()
        {
            SetChildActive(true);
            _collider = GetComponent<Collider>();
            _btn = GetComponent<WorldButton>();
            _btn.SubscibeOnClickHandler(this);
            _btn.enabled = pickable;
        }

        private void OnDisable()
        {
            SetChildActive(false);
            _btn.UnsubscribeOnClickHandler(this);
        }
#endregion //Unity MonoBehaviur Events

        private void SetChildActive(bool value)
        {
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(value);
            }
        }



        public void OnWorldButtonClickHandler()
        {
            Debug.Log("On Item Click", this);
        }
    }


}


