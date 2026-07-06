using UnityEngine;

namespace ObservedProject
{
    public class ScoreManagerGameA : MonoBehaviour
    {
        public static ScoreManagerGameA Instance;
        public bool isEndGame;
        public int finalScore = 0;

        private void Awake()
        {
            if (Instance != null)
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

