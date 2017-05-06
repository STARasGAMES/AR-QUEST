using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ItemsGridController : MonoBehaviour {

    private GridLayoutGroup _gridLayoutGroup;
    private RectTransform _rectTransform;
    private RectTransform _parentRectTransform;
    private void OnEnable()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _rectTransform = GetComponent<RectTransform>();
        _parentRectTransform = _rectTransform.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update () {
        float elementHeight = _rectTransform.rect.height - _gridLayoutGroup.padding.top - _gridLayoutGroup.padding.bottom;
        _gridLayoutGroup.cellSize = Vector2.one * elementHeight;
        int childCount = _rectTransform.childCount;
        float sizeDeltaX = childCount * elementHeight + _gridLayoutGroup.padding.left + _gridLayoutGroup.padding.right + _gridLayoutGroup.spacing.x * (childCount - 1);
        sizeDeltaX = Mathf.Clamp(sizeDeltaX, _parentRectTransform.rect.width, sizeDeltaX - _rectTransform.anchoredPosition.x);
        _rectTransform.sizeDelta = new Vector2(sizeDeltaX, _rectTransform.sizeDelta.y);

    }
}
