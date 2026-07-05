using Study.Utilities;
using System.Collections;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ObservedProject
{
    public class MiniGameAManager : MonoBehaviour
    {
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

        private int horseNumber;
        private TimeFlow timeFlow;

        private void Awake()
        {
            timeFlow = GetComponent<TimeFlow>();
            betPopUpImage.SetActive(true);
            checkPopUpImage.SetActive(false);
            resultPopUpImage.SetActive(false);
            SetActiveChoice(false);
            horses = new Button[] { horseNumber1, horseNumber2, horseNumber3, horseNumber4 };
        }

        private void Start()
        {
            ClickHorseButton();
            ClickGoButton();
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
                    conditionDescription.text = "컨디션 : 어떠함1"; // 이건 나중에!
                    break;
                case 1:
                    conditionDescription.text = "컨디션 : 어떠함2";
                    break;
                case 2:
                    conditionDescription.text = "컨디션 : 어떠함3";
                    break;
                case 3:
                    conditionDescription.text = "컨디션 : 어떠함4";
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
            checkPopUpImage.SetActive(false);
            StartCoroutine(timeFlow.CountDownCoroutine());
        }

        private void InteractNoButton()
        {
            checkPopUpImage.SetActive(false);
            betPopUpImage.SetActive(true);
            SetActiveChoice(false);
            ClickHorseButton();
            ClickGoButton();
        }

        #endregion


        #region Result PopUp Image

        public void CheckRank()
        {
            for (int i = 0; i < horseMoves.Length; ++i)
            {
                if (horseMoves[i].isGoal == false)
                {
                    Debug.Log($"{horseMoves[i].horseNumber}번 말 도착 안함!");
                    return;
                }
            }

            for (int i = 0; i < horseMoves.Length; ++i)
            {
                if (horseMoves[i].horseNumber == horseNumber)
                {
                    Debug.Log($"horseMoves[{i}].horseNumber = {horseMoves[i].horseNumber}");
                    Debug.Log($"horseNumber = {horseNumber}");
                    resultPopUpImage.SetActive(true);
                    rankText.text = $"{horseMoves[i].horseRank}위";
                    rewardText.text = "nnnn pt 획득!";
                    StartCoroutine(EndGameCoroutine());
                    return;
                }
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
