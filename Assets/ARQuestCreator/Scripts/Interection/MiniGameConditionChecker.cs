using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARQuestCreator.MiniGames;

namespace ARQuestCreator
{
    public class MiniGameConditionChecker : MonoBehaviour, IConditionChecker
    {

        [SerializeField] GameObject[] _miniGames;
        private IMiniGameManager[] _miniGamesManagers;

        // Use this for initialization
        void Start()
        {
            List<IMiniGameManager> games = new List<IMiniGameManager>();
            foreach(var g in _miniGames)
            {
                if (g.GetComponent<IMiniGameManager>() != null)
                {
                    games.Add(g.GetComponent<IMiniGameManager>());
                }
            }
            _miniGamesManagers = games.ToArray();
        }

        public bool IsSatisfied()
        {
            for (int i=0; i<_miniGamesManagers.Length; i++)
            {
                if (!_miniGamesManagers[i].IsGamePassed())
                    return false;
            }
            return true;
        }
    }
}
