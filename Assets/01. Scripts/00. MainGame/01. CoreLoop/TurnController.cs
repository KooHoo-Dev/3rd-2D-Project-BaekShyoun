using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ObservedProject
{
    public class TurnController : MonoBehaviour
    {
        [Header("TimeSettings")]
        public TextMeshProUGUI TimeCheck;
        public TextMeshProUGUI TimeCheckInStatus;
        public int turnCount = 1;
        private int dayCount;
        private string[] Hours = { "00:00", "03:00", "06:00", "09:00", "12:00", "15:00", "18:00", "21:00" };
        private int currentHourIndex;

        [Header("BecomeZeroValue")]
        public float MeatOrWaterDecreaseEnergy = 0.1f;

        [Header("OtherSettings")]
        public BeastController beastController;
        private GageController gageController;
        private DiseaseController diseaseController;
        private FadeController fadeController;

        public bool isTimeFlow = false;

        private void Awake()
        {
            gageController = GetComponent<GageController>();
            diseaseController = GetComponent<DiseaseController>();
            fadeController = GetComponentInChildren<FadeController>();
            dayCount = 1;
            currentHourIndex = 0;

            SetText();
        }

        private void Start()
        {
            StartCoroutine(fadeController.FadeOutCoroutine());
        }

        public void FlowTime(int skipTime)
        {
            currentHourIndex += skipTime;
            if (currentHourIndex >= Hours.Length)
            {
                currentHourIndex -= Hours.Length;
                dayCount++;
                diseaseController.RemoveDiseaseStack();
            }
            turnCount++;
            SetText();
            isTimeFlow = true;
            beastController.ChangedTurn(skipTime);
            BecomeZeroValue(gageController.fullnessBar, skipTime);
            BecomeZeroValue(gageController.hidrationBar, skipTime);
        }

        public void OnPlayGame()
        {
            FlowTime(1);
            gageController.PlayGame();
        }

        public void OnNormalSleep()
        {
            StartCoroutine(OnNormalSleepCoroutine());
        }

        private IEnumerator OnNormalSleepCoroutine()
        {
            yield return StartCoroutine(fadeController.FadeInAndOutCoroutine(1f));
            FlowTime(2);
            gageController.HaveNormalSleep();
            if(beastController.hasBeast)
            {
                beastController.SpawnBeast();
            }
        }

        public void OnDeepSleep()
        {
            StartCoroutine(OnDeepSleepCoroutine());
            
        }

        private IEnumerator OnDeepSleepCoroutine()
        {
            yield return StartCoroutine(fadeController.FadeInAndOutCoroutine(1f));
            FlowTime(2);
            gageController.HaveDeepSleep();
            if (beastController.hasBeast)
            {
                beastController.SpawnBeast();
            }
        }

        private void BecomeZeroValue(Image bar, int skipTime)
        {
            if (bar.fillAmount <= gageController.minAmount)
                gageController.GageUpdate(gageController.energyBar, (-MeatOrWaterDecreaseEnergy) * skipTime);
        }

        private void SetText()
        {
            TimeCheck.text = $"Day {dayCount} {Hours[currentHourIndex]}";
            TimeCheckInStatus.text = $"Day {dayCount} {Hours[currentHourIndex]}";
        }
    }
}