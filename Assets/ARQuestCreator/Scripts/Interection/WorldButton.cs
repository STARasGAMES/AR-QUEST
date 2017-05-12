using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class WorldButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    private delegate void OnWorldButtonClick();
    private OnWorldButtonClick clickHandlers;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _collider.enabled = true;
        Debug.Log("WorldButton "+name+" OnEnable " + _collider.enabled);
    }

    private void OnDisable()
    {
        _collider.enabled = false;
        Debug.Log("WorldButton " + name + " OnDisable " + _collider.enabled);
    }

    public void SubscribeOnClickHandler(IWorldButtonClickHandler handler)
    {
        clickHandlers += handler.OnWorldButtonClickHandler;
    }

    public void UnsubscribeOnClickHandler(IWorldButtonClickHandler handler)
    {
        clickHandlers -= handler.OnWorldButtonClickHandler;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick", this);              
        if (clickHandlers != null)
            clickHandlers();
        else
            Debug.LogError("No OnClick subscriber", this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown", this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp", this);
    }
}
