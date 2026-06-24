using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PracticeProject
{
    public class TurnController : MonoBehaviour
    {
        [Header("TimeSettings")]
        public TextMeshProUGUI TimeCheck;
        public TextMeshProUGUI TurnCheck;
        public Button[] GameButtons;
        public int TurnCount = 1;
        private int dayCount;
        private string[] Hours = { "00:00", "03:00", "06:00", "09:00", "12:00", "15:00", "18:00", "21:00" };
        private int currentHourIndex;

        [Header("SleepButtons")]
        public Button NormalSleepButton;
        public Button DeepSleepButton;

        [Header("BecomeZeroValue")]
        public float MeatOrWaterDecreaseEnergy = 10f;


        private GageController gageController;
        private DiseaseController diseaseController;

        private void Awake()
        {
            gageController = GetComponent<GageController>();
            diseaseController = GetComponent<DiseaseController>();
            dayCount = 1;
            currentHourIndex = 0;

            SetText();

            for (int i = 0; i < GameButtons.Length; i++)
            {
                GameButtons[i].onClick.AddListener(OnPlayGame);
            }
            NormalSleepButton.onClick.AddListener(OnNormalSleep);
            DeepSleepButton.onClick.AddListener(OnDeepSleep);
        }

        private void FlowTime(int skipTime)
        {
            currentHourIndex += skipTime;
            if (currentHourIndex >= Hours.Length)
            {
                currentHourIndex -= Hours.Length;
                dayCount++;
                diseaseController.RemoveDiseaseStack();
            }
            TurnCount++;
            SetText();
            gageController.WantToEscape(skipTime);

            BecomeZeroValue(gageController.fullnessBar, skipTime);
            BecomeZeroValue(gageController.hidrationBar, skipTime);
        }

        private void OnPlayGame()
        {
            FlowTime(1);
            gageController.PlayGame();
        }

        private void OnNormalSleep()
        {
            FlowTime(2);
            gageController.HaveNormalSleep();
        }

        private void OnDeepSleep()
        {
            FlowTime(2);
            gageController.HaveDeepSleep();
        }

        private void BecomeZeroValue(Slider bar, int skipTime)
        {
            if (bar.value <= 0)
                gageController.GageUpdate(gageController.energyBar, (-MeatOrWaterDecreaseEnergy) * skipTime);
        }

        private void SetText()
        {
            TimeCheck.text = $"Day {dayCount}, {Hours[currentHourIndex]}";
            TurnCheck.text = $"Turn {TurnCount}";
        }
    }
}