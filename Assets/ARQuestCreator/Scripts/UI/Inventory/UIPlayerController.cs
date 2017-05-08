using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQuestCreator
{
    public class UIPlayerController : MonoBehaviour, IUIController
    {
        private Animator _animator;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
        }

        public void Hide()
        {
            _animator.SetBool("Showing", false);
        }

        public void Show()
        {
            _animator.SetBool("Showing", true);
        }
    }
}
