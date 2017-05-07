using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class BoxController : MonoBehaviour, IWorldButtonClickHandler {

    public WorldButton openButton;
    [SerializeField]
    bool _isOpened = false;
    private Animation _anim;
    [Header("Animation names")]
    [SerializeField]
    string _nameOpen = "Open";
    [SerializeField]
    string _nameClose = "Close";
    [SerializeField]
    string _nameIdle = "Idle";

    public void OnWorldButtonClickHandler()
    {
        _isOpened = !_isOpened;
        if (_isOpened)
        {
            _anim.Play(_nameOpen, PlayMode.StopAll);
        }
        else
        {
            _anim.Play(_nameClose, PlayMode.StopAll);
        }
    }

    private void OnEnable()
    {
        _anim = GetComponent<Animation>();
        _anim.Play(_nameIdle, PlayMode.StopAll);
        _isOpened = false;
        openButton.SubscibeOnClickHandler(this);
    }

    private void OnDisable()
    {
        _anim.Play(_nameIdle, PlayMode.StopAll);
        _isOpened = false;
        openButton.UnsubscribeOnClickHandler(this);
    }

}
