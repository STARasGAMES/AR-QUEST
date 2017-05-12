using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQuestCreator.MiniGames.TubeGame
{
    public class FourDirTubeController : BaseTubeController
    {
        private readonly Vector2[] giveDirs = new Vector2[] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        public override void Rotate(float angle = 90)
        {
            Debug.Log("Cant rotate four dir tube");
        }

        protected override Vector2[] GetDefaultGiveDirections()
        {
            return giveDirs;
        }
    }
}
