using Study.Utilities;
using System.Collections;
using System.Reflection;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ObservedProject
{
    public class MiniGameAManager : MonoBehaviour
    {
        #region Inspector
        [Header("BetPopUpSettings")]
        public GameObject betPopUpImage;
        public Button horseNumber1;
        public Button horseNumber2;
        public Button horseNumber3;
        public Button horseNumber4;
        public Button[] horses;
        public TextMeshProUGUI conditionDescription;
        public Button goButton;

        [Header("CheckPopUpSettings")]
        public GameObject checkPopUpImage;
        public TextMeshProUGUI checkChoiceText;
        public Button yesButton;
        public Button noButton;

        [Header("ResultPopUpSettings")]
        public GameObject resultPopUpImage;
        public HorseMove[] horseMoves;
        public TextMeshProUGUI rankText;
        public TextMeshProUGUI rewardText;

        public int score { get; private set; }
        private int horseNumber;
        private TimeFlow timeFlow;
        private bool isOver = false;
        private bool isSaved = false;

        #endregion

        private void Awake()
        {
            timeFlow = GetComponent<TimeFlow>();
            betPopUpImage.SetActive(true);
            checkPopUpImage.SetActive(false);
            resultPopUpImage.SetActive(false);
            SetActiveChoice(false);
        }

        private void Start()
        {
            ClickHorseButton();
            ClickGoButton();
        }

        private void Update()
        {
            if (isOver)
            {
                if (SimpleInput.AnyKeyDown())
                {
                    if (isSaved == false)
                    {
                        isSaved = true;
                        ScoreManagerGameA.Instance.SaveScore(score);
                        ScoreManagerGameA.Instance.isEndGame = true;
                    }
                    SceneManager.UnloadSceneAsync(2);
                }
            }
        }

        #region Bet PopUp Image
        private void SetActiveChoice(bool isActive)
        {
            conditionDescription.gameObject.SetActive(isActive);
            goButton.gameObject.SetActive(isActive);
        }

        private void SetDescription(int horseIndex)
        {
            switch(horseIndex)
            {
                case 0:
                    SetText(horseMoves[horseIndex].state);
                    break;
                case 1:
                    SetText(horseMoves[horseIndex].state);
                    break;
                case 2:
                    SetText(horseMoves[horseIndex].state);
                    break;
                case 3:
                    SetText(horseMoves[horseIndex].state);
                    break;
            }
        }

        private void SetHorseButton(Button button, int horseIndex)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                horseNumber = horseIndex + 1;
                SetActiveChoice(true);
                SetDescription(horseIndex);
            });
        }

        public void SetText(HorseMove.HorseState state)
        {
            switch (state)
            {
                case HorseMove.HorseState.Perfect:
                    conditionDescription.text = "컨디션 : 최고";
                    break;
                case HorseMove.HorseState.Good:
                    conditionDescription.text = "컨디션 : 좋음";
                    break;
                case HorseMove.HorseState.NotBad:
                    conditionDescription.text = "컨디션 : 나쁘지 않음";
                    break;
                case HorseMove.HorseState.Bad:
                    conditionDescription.text = "컨디션 : 나쁨";
                    break;
            }
        }

        private void ClickHorseButton()
        {
            for (int i = 0; i < horses.Length; ++i)
            {
                int index = i;
                SetHorseButton(horses[index], index);
            }
        }

        private void ClickGoButton()
        {
            goButton.onClick.AddListener(SetGoInteraction);
        }

        #endregion

        /// <summary>
        /// BetPopUp > CheckPopUp
        /// </summary>
        private void SetGoInteraction()
        {
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
            betPopUpImage.SetActive(false);
            checkPopUpImage.SetActive(true);
            checkChoiceText.text = $"{horseNumber}번 말에 베팅하시겠습니까?";
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(InteractYesButton);
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(InteractNoButton);
        }


        #region Check PopUp Image

        private void InteractYesButton()
        {
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
            checkPopUpImage.SetActive(false);
            StartCoroutine(timeFlow.CountDownCoroutine());
        }

        private void InteractNoButton()
        {
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
            checkPopUpImage.SetActive(false);
            betPopUpImage.SetActive(true);
            SetActiveChoice(false);
            ClickHorseButton();
            ClickGoButton();
        }

        #endregion


        #region Result PopUp Image & End Game

        public void CheckRank()
        {
            for (int i = 0; i < horseMoves.Length; ++i)
            {
                if (horseMoves[i].isGoal == false)
                {
                    return;
                }
            }

            for (int i = 0; i < horseMoves.Length; ++i)
            {
                if (horseMoves[i].horseNumber == horseNumber)
                {
                    GameSoundManager.Instance.StopLoop();
                    GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.audience);

                    resultPopUpImage.SetActive(true);
                    rankText.text = $"{horseMoves[i].horseRank}위";
                    SetScoreByState(horseMoves[i].state, horseMoves[i].horseRank);

                    if (horseMoves[i].horseRank <= 2)
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.raceSuccess);
                    else
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.raceFail);

                    rewardText.text = $"{score} pt 획득!";
                    isOver = true;
                    return;
                }
            }
        }

        public void SetScoreByState(HorseMove.HorseState state, int rank)
        {
            switch(state)
            {
                case HorseMove.HorseState.Perfect:
                    SetScore(rank, 1000, 400, 100, -100);
                    break;
                case HorseMove.HorseState.Good:
                    SetScore(rank, 1200, 500, 100, 0);
                    break;
                case HorseMove.HorseState.NotBad:
                    SetScore(rank, 1400, 600, 100, 0);
                    break;
                case HorseMove.HorseState.Bad:
                    SetScore(rank, 2000, 800, 100, 0);
                    break;
            }
        }

        private void SetScore(int rank, int firstScore, int secondScore, int thirdScore, int forthScore)
        {
            switch (rank)
            {
                case 1:
                    score = firstScore;
                    break;
                case 2:
                    score = secondScore;
                    break;
                case 3:
                    score = thirdScore;
                    break;
                case 4:
                    score = forthScore;
                    break;
            }
        }

        public IEnumerator EndGameCoroutine()
        {
            yield return new WaitUntil(() => SimpleInput.GetKeyDown(Key.Enter));
            SceneManager.UnloadSceneAsync(2);
        }

        #endregion
    }
}
