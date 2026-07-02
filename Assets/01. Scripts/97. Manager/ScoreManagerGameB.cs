using TMPro;
using UnityEngine;

namespace ObservedProject
{
    public class ScoreManagerGameB : MonoBehaviour
    {
        public static ScoreManagerGameB Instance;
        public bool IsEndGame;
        public int finalScore = 0;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public int SaveScore(int score)
        {
            finalScore = score;
            return finalScore;
        }

        public void DestroyManager()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
}