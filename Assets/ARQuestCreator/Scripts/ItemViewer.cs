using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

namespace ARQuestCreator
{
    public class ItemViewer : Singleton<ItemViewer> {

        [SerializeField] float _rotationSens = 1;
        [SerializeField] float _maxFoV = 60;
        [SerializeField] float _startScale = 3;
        [SerializeField] Camera _camera;
        [SerializeField] bool _ignoreGuiFingers = false;
        [SerializeField] Vector3 RotateAxis = Vector3.forward;
        private Transform _transform;
        public Item currentItem { get; private set; }
        private Vector3 _startPos;

        Transform[] _transforms;
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
            var fingers = LeanTouch.GetFingers(_ignoreGuiFingers, 1);

            // Calculate the screenDelta value based on these fingers
            var screenDelta = LeanGesture.GetScreenDelta(fingers) * _rotationSens;
            _transform.RotateAround(_transform.position, Vector3.up, -screenDelta.x);
            _transform.RotateAround(_transform.position, Vector3.right, screenDelta.y);
            
        }

        private bool Scale()
        {
            // Get the fingers we want to use
            var fingers = LeanTouch.GetFingers(_ignoreGuiFingers, 2);
            
            // Calculate the scaling values based on these fingers
            var scale = LeanGesture.GetPinchScale(fingers, 0);
            if (scale == 1)
                return false;
            float newScale = _camera.fieldOfView / scale;
            newScale = Mathf.Clamp(newScale, 5, _maxFoV);
            _camera.fieldOfView = newScale;

            var degrees = LeanGesture.GetTwistDegrees(fingers);
            _transform.RotateAround(_transform.position, Vector3.forward, degrees);

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
            _camera.fieldOfView = _maxFoV;
            
            item.SetAtcive(true);

            Vector3 centerCorrection = item.transform.position - item.GetCenterInWorldSpace();
            Vector3 sizeCor = item.GetSizeInWorldSpace();
            float scale = Mathf.Sqrt(sizeCor.magnitude);
            item.transform.SetParent(_transform);
            item.transform.localPosition = centerCorrection;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localScale = item.transform.localScale / scale * _startScale;


            item.button.enabled = false;
            _transforms = item.GetComponentsInChildren<Transform>(true);
            _renderersLayers = new int[_transforms.Length];
            for(int i=0; i<_transforms.Length; i++)
            {
                _renderersLayers[i] = _transforms[i].gameObject.layer;
                _transforms[i].gameObject.layer = LayerMask.NameToLayer("ItemViewer");
            }
        }

        public Item GetBackItem()
        {
            if (currentItem == null)
                Debug.LogError("WTF");
            Item result = currentItem;
            currentItem = null;
            for (int i = 0; i < _transforms.Length; i++)
            {
                _transforms[i].gameObject.layer = _renderersLayers[i];
            }
            return result;
        }

        public bool IsEmpty()
        {
            return currentItem == null;
        }
    }
}
