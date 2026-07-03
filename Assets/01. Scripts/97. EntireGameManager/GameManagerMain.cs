using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ObservedProject
{
    public class GameManagerMain : MonoBehaviour
    {
        public static GameManagerMain Instance;
        public DiseaseController diseaseController;
        public BeastController beastController;

        public bool isDeadByDisease = false;
        public bool isDeadByBeast = false;

        public FadeController fadeController;

        private void Awake()
        {
            fadeController = GetComponentInChildren<FadeController>();
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 질병 스택 5로 게임 오버
        /// </summary>
        public void GameOverByDisease()
        {
            StartCoroutine(GameOverByDiseaseCoroutine());
        }

        public IEnumerator GameOverByDiseaseCoroutine()
        {
            if (diseaseController.diseaseStack >= 5)
            {
                isDeadByDisease = true;
                yield return StartCoroutine(fadeController.FadeInCoroutine());
                SceneManager.LoadScene(4);
            }
        }

        /// <summary>
        /// 맹수 탈출 시 맹수의 공격에 의해 게임 오버
        /// </summary>
        public void GameOverByBeast()
        {
            StartCoroutine(GameOverByBeastCoroutine());
        }

        public IEnumerator GameOverByBeastCoroutine()
        {
            Debug.Log(fadeController);
            Debug.Log(beastController);
            Debug.Log(diseaseController);
            int randNum = Random.Range(1, 101);
            if (randNum <= beastController.attackPercentage)
            {
                isDeadByBeast = true;
                yield return StartCoroutine(fadeController.FadeInCoroutine());
                SceneManager.LoadScene(4);
            }
        }

        public void DestroyManager()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
}