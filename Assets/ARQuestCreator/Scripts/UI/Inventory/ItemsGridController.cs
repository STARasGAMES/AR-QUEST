using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ItemsGridController : MonoBehaviour {

    private HorizontalLayoutGroup _horizontalLayoutGroup;
    private RectTransform _rectTransform;
    private RectTransform _parentRectTransform;
    [SerializeField] int _elementsPerView = 7;
    private void OnEnable()
    {
        _horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        _rectTransform = GetComponent<RectTransform>();
        _parentRectTransform = _rectTransform.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update () {
        float elementHeight = _parentRectTransform.rect.width / _elementsPerView;
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, elementHeight * _rectTransform.childCount);
        //_gridLayoutGroup.cellSize = Vector2.one * elementHeight;
        //int childCount = _rectTransform.childCount;
        //float sizeDeltaX = childCount * elementHeight + _gridLayoutGroup.padding.left + _gridLayoutGroup.padding.right + _gridLayoutGroup.spacing.x * (childCount - 1);
        //sizeDeltaX = Mathf.Clamp(sizeDeltaX, _parentRectTransform.rect.width, sizeDeltaX - _rectTransform.anchoredPosition.x);
        //_rectTransform.sizeDelta = new Vector2(sizeDeltaX, _rectTransform.sizeDelta.y);

    }
}
