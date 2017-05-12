using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARQuestCreator.MiniGames
{
    public interface IMiniGameManager
    {

        void LoadGame();

        bool IsGamePassed();
    }
}
