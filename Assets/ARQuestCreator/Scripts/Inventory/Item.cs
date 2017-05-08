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
        public bool immediatelyPickup = false;
        public bool canBeViewed = true;
        public bool rotatable = true;
        public bool scalable = true;

        public WorldButton button { get; private set; }

        private Transform _parent;
        private Vector3 _localPos;
        private Quaternion _localRot;
        private Vector3 _localScale;

        public enum ItemState
        {
            Inventory,
            World
        }
        public ItemState state = ItemState.World;


        #region Unity MonoBehaviur Events


        private void OnEnable()
        {
            SetChildActive(true);
            button = GetComponent<WorldButton>();
            button.SubscibeOnClickHandler(this);
            button.enabled = pickable;
        }

        private void OnDisable()
        {
            SetChildActive(false);
            button.UnsubscribeOnClickHandler(this);
        }

        private void Awake()
        {
            ApplyCurrentParent();
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
            if (immediatelyPickup)
                GameManager.Instance.PickupItem(this);
            else
                GameManager.Instance.ViewItem(this);
        }

        public void ApplyCurrentParent()
        {
            _parent = transform.parent;
            _localPos = transform.localPosition;
            _localRot = transform.localRotation;
            _localScale = transform.localScale;
        }

        public void GoToApplyedParent()
        {
            transform.parent = _parent;
            transform.localPosition = _localPos;
            transform.localRotation = _localRot;
            transform.localScale = _localScale;
        }
    }


}


