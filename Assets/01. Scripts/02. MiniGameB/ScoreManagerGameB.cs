using TMPro;
using UnityEngine;

namespace ObservedProject
{
    public class ScoreManagerGameB : MonoBehaviour
    {
        public GameObject scoreManager;
        private MiniGameBManager gameManager;
        public int finalScore = 0;

        private void Awake()
        {
            gameManager = scoreManager.GetComponentInParent<MiniGameBManager>();
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