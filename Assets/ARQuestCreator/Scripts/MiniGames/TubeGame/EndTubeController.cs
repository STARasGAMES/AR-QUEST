using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQuestCreator.MiniGames.TubeGame
{
    public class EndTubeController : BaseTubeController
    {

        private readonly Vector2[] giveDirs = new Vector2[] { Vector2.right };

        public override void Rotate(float angle = 90)
        {
            Debug.Log("Cant rotate end tube!");
        }

        protected override Vector2[] GetDefaultGiveDirections()
        {
            return giveDirs;
        }

        



    }
}
