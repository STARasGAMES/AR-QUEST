using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemController : MonoBehaviour {

    [SerializeField] Text _itemNameText;
    [SerializeField] Image _itemImage;

    public void ShowItem()
    {
        gameObject.SetActive(true);
    }

}
