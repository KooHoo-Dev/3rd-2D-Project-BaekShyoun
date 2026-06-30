using TMPro;
using UnityEngine;

namespace ObservedProject
{
    public class ScoreManagerGameB : MonoBehaviour
    {
        public GameObject scoreManager;
        public MiniGameBManager gameManager;
        public int finalScore = 0;

        private void Awake()
        {
            DontDestroyOnLoad(scoreManager);
        }

        private void Update()
        {
            UpdateScore();
        }

        public int UpdateScore()
        {
            finalScore = gameManager.currentScore;
            return finalScore;
        }
    }
}