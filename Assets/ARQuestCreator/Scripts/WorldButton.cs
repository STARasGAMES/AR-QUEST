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

    private void OnEnable()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = true;
    }

    private void OnDisable()
    {
        _collider.enabled = false;
    }

    public void SubscibeOnClickHandler(IWorldButtonClickHandler handler)
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
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
