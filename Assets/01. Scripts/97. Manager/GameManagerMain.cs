using UnityEngine;
using UnityEngine.SceneManagement
;

namespace ObservedProject
{
    public class GameManagerMain : MonoBehaviour
    {
        public static GameManagerMain Instance;
        public DiseaseController diseaseController;
        public BeastController beastController;

        public bool isDeadByDisease = false;
        public bool isDeadByBeast = false;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 질병 스택 5로 게임 오버
        /// </summary>
        public void GameOverByDisease()
        {
            if(diseaseController.diseaseStack>=5)
            {
                isDeadByDisease = true;
                SceneManager.LoadScene(4);
            }
        }

        /// <summary>
        /// 맹수 탈출 시 맹수의 공격에 의해 게임 오버
        /// </summary>
        public void GameOverByBeast()
        {
            int randNum = Random.Range(1, 101);
            if (randNum <= beastController.attackPercentage)
            {
                isDeadByBeast = true;
                SceneManager.LoadScene(4);
            }
        }
    }
}