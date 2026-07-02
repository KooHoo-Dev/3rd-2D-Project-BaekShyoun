using Study.Utilities;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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

        [Header("EventScript")]
        public TextMeshProUGUI eventScriptText;

        private DiseaseController diseaseController;
        private FadeController fadeController;
        private UIInteraction uiInteraction;
        private TurnController turnController;
        private bool isBlackOut = false;

        private void Awake()
        {
            turnController = GetComponent<TurnController>();
            diseaseController = GetComponent<DiseaseController>();
            fadeController = GetComponentInChildren<FadeController>();
            uiInteraction = GetComponentInChildren<UIInteraction>();
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

        public void GetBlackOut()
        {
            if (isBlackOut) return;

            eventScriptText.text = string.Empty;
            StartCoroutine(GetBlackOutCoroutine());
        }
        #endregion

        public IEnumerator GetBlackOutCoroutine()
        {
            yield return StartCoroutine(fadeController.FadeInCoroutine());
            uiInteraction.ActiveEventScriptScreen();
            eventScriptText.text = "휴식을 제대로 취하지 못한 당신은 정신을 잃고 쓰러집니다.";
            GageUpdate(fullnessBar, -gameDecreaseFullness * 2);
            GageUpdate(hidrationBar, -gameDecreaseHidration * 2);
            turnController.FlowTime(2);
            yield return new WaitUntil(()=>(SimpleInput.GetKeyDown(Key.Enter) || SimpleInput.GetKeyDown(Key.Space)));
            yield return StartCoroutine(fadeController.FadeOutCoroutine());

            isBlackOut = false;
        }

        public void GageUpdate(Image bar, float fillAmount)
        {
            fillAmount = diseaseController.ChangeStatIfSick(fillAmount);
            float currentValue = bar.fillAmount;
            currentValue += fillAmount;
            if (currentValue >= maxAmount) currentValue = maxAmount;
            if (currentValue <= minAmount) currentValue = minAmount;
            if (energyBar.fillAmount <= minAmount)
                GetBlackOut();
            bar.fillAmount = currentValue;
        }
    }
}

