using UnityEngine;
using UnityEngine.UI;

namespace PracticeProject
{
    public class GageController : MonoBehaviour
    {
        [Header("PlayerSliderSettings")]
        public Slider fullnessBar;
        public Slider hidrationBar;
        public Slider energyBar;

        [Header("BeastSliderSettings")]
        public Slider escapeBar;
        public Slider attackPercentageBar;

        [Header("GameDesireSettings")]
        public float gameDecreaseFullness = 30f;
        public float gameDecreaseHidration = 10f;
        public float gameDecreaseEnergy = 30f;

        [Header("SleepDesireSettings")]
        public float sleepDecreaseFullness = 50f;
        public float sleepDecreaseHidration = 30f;
        public float normalSleepIncreaseEnergy = 20f;
        public float deepSleepIncreaseEnergy = 40f;

        [Header("BeastSettings")]
        public int escapeDesire = 20;

        private DiseaseController diseaseController;

        private void Awake()
        {
            diseaseController = GetComponent<DiseaseController>();
        }

        private void Start()
        {
            // 최대값
            fullnessBar.maxValue = 100f;
            hidrationBar.maxValue = 100f;
            energyBar.maxValue = 100f;
            escapeBar.maxValue = 100f;
            attackPercentageBar.maxValue = 100f;
            // 최소값
            fullnessBar.minValue = 0f;
            hidrationBar.minValue = 0f;
            energyBar.minValue = 0f;
            escapeBar.minValue = 0f;
            attackPercentageBar.minValue = 0f;
            // 기본값
            fullnessBar.value = 100f;
            hidrationBar.value = 100f;
            energyBar.value = 100f;
            escapeBar.value = 0f;
            attackPercentageBar.value = 0f;
        }

        #region ForPlayer
        public void PlayGame()
        {
            GageUpdate(fullnessBar, -gameDecreaseFullness);
            GageUpdate(hidrationBar, -gameDecreaseHidration);
            GageUpdate(energyBar, -gameDecreaseEnergy);
        }

        public void HaveNormalSleep()
        {
            GageUpdate(fullnessBar, -sleepDecreaseFullness);
            GageUpdate(hidrationBar, -sleepDecreaseHidration);
            GageUpdate(energyBar, normalSleepIncreaseEnergy);
        }

        public void HaveDeepSleep()
        {
            GageUpdate(fullnessBar, -sleepDecreaseFullness);
            GageUpdate(hidrationBar, -sleepDecreaseHidration);
            GageUpdate(energyBar, deepSleepIncreaseEnergy);
        }
        #endregion

        #region ForBeast
        public void WantToEscape(int skipTime)
        {
            escapeDesire *= skipTime;
            escapeBar.value += escapeDesire;
        }


        #endregion
        public void GageUpdate(Slider bar, float amount)
        {
            amount = diseaseController.ChangeStatIfSick(amount);
            float currentValue = bar.value;
            currentValue += amount;
            if (currentValue >= bar.maxValue) currentValue = bar.maxValue;
            if (currentValue <= bar.minValue) currentValue = bar.minValue;
            bar.value = currentValue;
        }

    }
}