using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace ARQuestCreator
{
    
    [RequireComponent(typeof(BoxCollider))]
    public class Item : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        public string name = "defaultItemName";
        [TextArea(3, 20)]
        public string description = "default description";
        public bool pickable = true;
        public bool canBeViewed = true;
        public bool rotatable = true;
        public bool scalable = true;

        private Collider _collider;

#region Unity MonoBehaviur Events
        private void OnEnable()
        {
            SetChildActive(true);
            _collider = GetComponent<Collider>();
            _collider.enabled = pickable;
        }

        private void OnDisable()
        {
            SetChildActive(false);
        }
#endregion //Unity MonoBehaviur Events

        private void SetChildActive(bool value)
        {
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(value);
            }
        }

#region IPointerHandlerImplementation

        public void OnPointerClick(PointerEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
           // throw new NotImplementedException();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
          //  throw new NotImplementedException();
        }
    }
#endregion // IPointerHandlerImplementation

   
}


