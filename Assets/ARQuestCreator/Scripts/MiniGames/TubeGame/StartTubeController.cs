using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQuestCreator.MiniGames.TubeGame
{
    public class StartTubeController : BaseTubeController
    {
        public override void Rotate(float angle = 90)
        {
            Debug.Log("Cant rotate start tube");
        }

        private readonly Vector2[] giveDirs = new Vector2[] { Vector2.right };

        protected override Vector2[] GetDefaultGiveDirections()
        {
            return giveDirs;
        }
    }
}
