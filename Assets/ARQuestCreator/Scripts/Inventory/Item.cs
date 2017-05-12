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
        public bool single = true;
        public int count;
        public bool pickable = true;
        public bool immediatelyPickup = false;
        public bool canBeViewed = true;
        public bool rotatable = true;
        public bool scalable = true;

        [SerializeField] List<Renderer> _renderers = new List<Renderer>();

        public WorldButton button { get; private set; }

        private Transform _parent;
        private Vector3 _localPos;
        private Quaternion _localRot;
        private Vector3 _localScale;
        private bool _activeSelf;

        public enum ItemState
        {
            Inventory,
            World
        }
        public ItemState state = ItemState.World;


        #region Unity MonoBehaviur Events

       

        private void OnEnable()
        {
            Debug.Log("OnEnable " + name, this);
            button.enabled = pickable;
            //SetChildActive(true);
            
        }

        private void OnDisable()
        {
            //SetChildActive(false);
            button.enabled = false;
            Debug.Log("OnDisable " + name, this);
        }

        private void OnDestroy()
        {

            button.UnsubscribeOnClickHandler(this);
        }

        private void Awake()
        {
            ApplyCurrentParent();
            button = GetComponent<WorldButton>();
            button.SubscribeOnClickHandler(this);
            button.enabled = pickable;
            if (state == ItemState.Inventory)
            {
                PlayerInventory.Instance.AddItem(this);
            }
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
            Debug.Log("On Item click "+name, this);
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
            _activeSelf = gameObject.activeSelf;
        }

        public void GoToApplyedParent()
        {
            transform.parent = _parent;
            transform.localPosition = _localPos;
            transform.localRotation = _localRot;
            transform.localScale = _localScale;
            gameObject.SetActive(_activeSelf);
        }

        public void SetAtcive(bool value)
        {
            gameObject.SetActive(value);
        }

        public Vector3 GetCenterInWorldSpace()
        {
            Bounds b = _renderers[0].bounds;
            foreach(var r in _renderers)
            {
                b.Encapsulate(r.bounds);
            }
            Debug.Log(transform.position + "  " + b.center);
            return b.center;
        }

        public Vector3 GetSizeInWorldSpace()
        {
            Bounds b = _renderers[0].bounds;
            foreach (var r in _renderers)
            {
                b.Encapsulate(r.bounds);
            }
            Debug.Log(transform.position + "  " + b.center);
            return b.size;
        }
    }


}


