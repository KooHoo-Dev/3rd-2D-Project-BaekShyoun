using UnityEngine;
using UnityEngine.UI;

namespace ObservedProject
{
    public class GageController : MonoBehaviour
    {
        [Header("PlayerSliderSettings")]
        public Image fullnessBar;
        public Image hidrationBar;
        public Image energyBar;
        private Image[] playerDesire;
        public float maxAmount { get; private set; } = 1f;
        public float minAmount { get; private set; } = 0f;

        [Header("GameDesireSettings")]
        public float gameDecreaseFullness = 0.3f;
        public float gameDecreaseHidration = 0.1f;
        public float gameDecreaseEnergy = 0.3f;

        [Header("SleepDesireSettings")]
        public float sleepDecreaseFullness = 0.5f;
        public float sleepDecreaseHidration = 0.3f;
        public float normalSleepIncreaseEnergy = 0.2f;
        public float deepSleepIncreaseEnergy = 0.4f;

        private DiseaseController diseaseController;

        private void Awake()
        {
            diseaseController = GetComponent<DiseaseController>();
        }

        private void Start()
        {
            playerDesire = new Image[] { fullnessBar, hidrationBar, energyBar };
            for (int i = 0; i < playerDesire.Length; i++)
            {
                playerDesire[i].fillAmount = maxAmount;
            }
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

        public void GageUpdate(Image bar, float fillAmount)
        {
            fillAmount = diseaseController.ChangeStatIfSick(fillAmount);
            float currentValue = bar.fillAmount;
            currentValue += fillAmount;
            if (currentValue >= maxAmount) currentValue = maxAmount;
            if (currentValue <= minAmount) currentValue = minAmount;
            bar.fillAmount = currentValue;
        }

        
    
    }
}

