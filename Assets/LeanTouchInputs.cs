using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
namespace ARQuestCreator
{
    public class LeanTouchInputs : MonoBehaviour
    {

        protected virtual void OnEnable()
        {
            // Hook into the events we need
            LeanTouch.OnFingerSwipe += OnFingerSwipe;
        }

        protected virtual void OnDisable()
        {
            // Unhook the events
            LeanTouch.OnFingerSwipe -= OnFingerSwipe;
        }

        public void OnFingerSwipe(LeanFinger finger)
        {
            return;
            // Store the swipe delta in a temp variable
            var swipe = finger.SwipeScreenDelta;

            if (swipe.x < -Mathf.Abs(swipe.y))
            {
                OnSwipeLeft();
            }

            if (swipe.x > Mathf.Abs(swipe.y))
            {
                OnSwipeRight();
            }

            if (swipe.y < -Mathf.Abs(swipe.x))
            {
                OnSwipeDown();
            }

            if (swipe.y > Mathf.Abs(swipe.x))
            {
                OnSwipeUp();
            }
        }

        private void OnSwipeLeft()
        {
            Debug.Log("OnSwipeLeft");
        }

        private void OnSwipeRight()
        {
            Debug.Log("OnSwipeRight");
        }

        private void OnSwipeUp()
        {
            //GameManager.Instance.OnInventoryShow();
            Debug.Log("OnSwipeUp");
        }

        private void OnSwipeDown()
        {
            //GameManager.Instance.OnInventoryHide();
            Debug.Log("OnSwipeDown");
        }
    }
}
