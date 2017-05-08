using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQuestCreator
{
    [RequireComponent(typeof(Animator))]
    public class ChestController : MonoBehaviour, IWorldButtonClickHandler
    {

        public WorldButton openButton;
        [SerializeField]
        bool _lockIsClosed = true;
        [SerializeField]
        bool _isOpened = false;
        [SerializeField]
        List<Item> _requiredItems;
        [Header("Animator properties")]
        [SerializeField] string _isOpenParamName = "IsOpen";

        private Animator _anim;

        public void OnWorldButtonClickHandler()
        {
            if (_lockIsClosed)
            {
                if (!PlayerInventory.Instance.ContainsItems(_requiredItems))
                    return;
                _lockIsClosed = false;
            }
            _isOpened = !_isOpened;
            _anim.SetBool(_isOpenParamName, _isOpened);
            
        }

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _lockIsClosed = true;
            _isOpened = false;
            _anim.SetBool(_isOpenParamName, _isOpened);
            openButton.SubscibeOnClickHandler(this);
        }

    }
}
