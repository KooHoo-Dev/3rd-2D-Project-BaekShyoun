using Study.Utilities;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ObservedProject
{
    public class MiniGameBManager : MonoBehaviour
    {
        #region Inspector
        public enum HeartState
        {
            Empty = 0,
            Full
        }

        [Header("UI Settings")]
        public TextMeshProUGUI ScoreText;
        public TextMeshProUGUI PressEnterToStartText;
        public TextMeshProUGUI GameOverText;
        public HeartController[] hearts;
        public Sprite fullHeart;
        public Sprite emptyHeart;

        public int currentScore { get; private set; } = 0;


        #endregion

        #region GameManager

        private OrderTurn orderTurn;
        private FoodChoice foodChoice;
        private TimeController timeController;
        private bool isOver = false;
        private bool isSaved = false;

        private void Awake()
        {
            PressEnterToStartText.gameObject.SetActive(true);
            GameOverText.gameObject.SetActive(false);
            ScoreText.text = $"SCORE {currentScore.ToString()}";
            orderTurn = GetComponentInChildren<OrderTurn>();
            foodChoice = GetComponentInChildren<FoodChoice>();
            timeController = GetComponentInChildren<TimeController>();
            hearts = GetComponentsInChildren<HeartController>();
            for (int i = 0; i < hearts.Length; ++i)
            {
                SetHeart(hearts[i], HeartState.Full);
            }
        }

        private void Start()
        {
            StartCoroutine(PressEnterToStart());
        }

        private void Update()
        {
            if(isOver)
            {
                if(SimpleInput.AnyKeyDown())
                {
                    if(isSaved == false)
                    {
                        isSaved = true;
                        ScoreManagerGameB.Instance.SaveScore(currentScore);
                        ScoreManagerGameB.Instance.isEndGame = true;
                    }
                    SceneManager.UnloadSceneAsync(3);
                }
            }
        }

        private IEnumerator PressEnterToStart()
        {
            yield return new WaitUntil(() => SimpleInput.GetKeyDown(Key.Enter));
            PressEnterToStartText.gameObject.SetActive(false);
            orderTurn.MakeOrder();
        }

        public void CheckAnswers()
        {
            for (int i = 0; i < orderTurn.orderedFoods.Length; ++i)
            {
                if (orderTurn.orderedFoods[i].currentFood != foodChoice.pickedFoods[i].currentFood)
                {
                    EraseHeart();
                    return;
                }
            }
            GetScore();
        }

        private void ResetAll()
        {
            timeController.ResetTimeBar();
            foodChoice.ResetFoodChoice();
            orderTurn.ResetOrderTurn();
            PressEnterToStartText.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(PressEnterToStart());
        }

        private void GetScore()
        {
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.correct);

            currentScore += 100;
            ScoreText.text = $"SCORE {currentScore.ToString()}";
            ResetAll();
        }

        public void EraseHeart()
        {
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.wrong);
            Debug.Log("Erase Heart!");
            if (hearts[2].state == HeartState.Full)
            {
                SetHeart(hearts[2], HeartState.Empty);
                ResetAll();
            }
            else if(hearts[1].state == HeartState.Full)
            {
                SetHeart(hearts[1], HeartState.Empty);
                ResetAll();
            }
            else if (hearts[0].state == HeartState.Full)
            {
                SetHeart(hearts[0], HeartState.Empty);
                ResetAll();
            }
            else
            {
                GameOver();
            }
        }

        private void SetHeart(HeartController heart, HeartState state)
        {
            heart.state = state;

            if (state == HeartState.Empty)
                heart.image.sprite = emptyHeart;
            else
                heart.image.sprite = fullHeart;
        }

        private void GameOver()
        {
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.heartEmpty);

            GameOverText.gameObject.SetActive(true);
            isOver = true;
        }
        #endregion
    }
}