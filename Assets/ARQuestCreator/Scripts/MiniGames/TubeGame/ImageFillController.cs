using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARQuestCreator.MiniGames.TubeGame
{
    public class ImageFillController : MonoBehaviour, IFillController
    {
        Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetFillAmount(float amount)
        {
            _image.fillAmount = amount;
        }
    }
}
