using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

namespace ARQuestCreator
{
    public class ItemViewer : Singleton<ItemViewer> {

        [SerializeField] float _rotationSens = 1;
        [SerializeField] float _maxScale = 3;
        [SerializeField] Camera _camera;
        private Transform _transform;
        public Item currentItem { get; private set; }
        private Vector3 _startPos;

        Renderer[] _renderers;
        int[] _renderersLayers;

        private void Start()
        {
            _transform = this.transform;
            _startPos = _transform.localPosition;
        }

        private void Update()
        {
            if (currentItem != null)
            {
                _camera.enabled = true;
                if (!Scale())
                    Rotate();
            }
            else
            {
                _camera.enabled = false;
            }
        }

        private void Rotate()
        {
            // Get the fingers we want to use
            var fingers = LeanTouch.GetFingers(false, 1);

            // Calculate the screenDelta value based on these fingers
            var screenDelta = LeanGesture.GetScreenDelta(fingers) * _rotationSens;
            _transform.RotateAround(_transform.position, Vector3.up, -screenDelta.x);
            _transform.RotateAround(_transform.position, Vector3.right, screenDelta.y);
            //_transform.eulerAngles += new Vector3(screenDelta.y, screenDelta.x) ;
        }

        private bool Scale()
        {
            // Get the fingers we want to use
            var fingers = LeanTouch.GetFingers(false, 2);
            
            // Calculate the scaling values based on these fingers
            var scale = LeanGesture.GetPinchScale(fingers, 0);
            if (scale == 1)
                return false;
            float newScale = _camera.fieldOfView / scale;
            newScale = Mathf.Clamp(newScale, 5, 50);
            _camera.fieldOfView = newScale;

            //float newScale = _transform.localScale.x * scale;
            //newScale = Mathf.Clamp(newScale, 0.1f, _maxScale);
            //_transform.localScale = Vector3.one * newScale;
            return true;
        }

        public void ViewItem(Item item)
        {
            if (currentItem != null)
                Debug.LogError("WTF");
            currentItem = item;
            _transform.localPosition = _startPos;
            _transform.localScale = Vector3.one;
            _transform.localEulerAngles = Vector3.up * 180;
            _camera.fieldOfView = 50;
            item.enabled = true;
            item.transform.SetParent(_transform);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            Vector3 globalCenter = GetCenterOfGO(item.gameObject);
            item.transform.localPosition -= _transform.InverseTransformPoint(globalCenter);
            item.button.enabled = false;
            _renderers = item.GetComponentsInChildren<Renderer>(true);
            _renderersLayers = new int[_renderers.Length];
            for(int i=0; i<_renderers.Length; i++)
            {
                _renderersLayers[i] = _renderers[i].gameObject.layer;
                _renderers[i].gameObject.layer = LayerMask.NameToLayer("ItemViewer");
            }
        }

        public Item GetBackItem()
        {
            if (currentItem == null)
                Debug.LogError("WTF");
            Item result = currentItem;
            currentItem = null;
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].gameObject.layer = _renderersLayers[i];
            }
            return result;
        }
        

        private Vector3 GetCenterOfGO(GameObject go)
        {
            Renderer[] rends = go.GetComponentsInChildren<Renderer>();
            Vector3 globalCenter = rends[0].bounds.center;
            for (int i=1; i<rends.Length; i++)
            {
                globalCenter += rends[i].bounds.center;
                globalCenter *= 0.5f;
            }
            return globalCenter;
        }

        public bool IsEmpty()
        {
            return currentItem == null;
        }
    }
}
