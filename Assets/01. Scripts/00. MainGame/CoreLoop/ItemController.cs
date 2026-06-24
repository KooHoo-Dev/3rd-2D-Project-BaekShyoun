using UnityEngine;
using UnityEngine.UI;

namespace ObservedProject
{
    public class ItemController : MonoBehaviour
    {
        public enum MeatType
        {
            Fresh,
            Suspicious,
            Rotten
        }

        public enum WaterType
        {
            Fresh,
            Rotten
        }

        [Header("MeatButtons")]
        public Button freshMeatButton;
        public Button suspiciousMeatButton;
        public Button rottenMeatButton;

        [Header("WaterButtons")]
        public Button freshWaterButton;
        public Button rottenWaterButton;

        [Header("DesireSettings")]
        public float meatIncreaseFullness = 0.2f;
        public float waterIncreaseHidration = 0.1f;

        private GageController gageController;
        private DiseaseController diseaseController;

        private void Awake()
        {
            gageController = GetComponent<GageController>();
            diseaseController = GetComponent<DiseaseController>();

            // Rotten 비활성화 > 인벤 로직 짜기 전까진 각주 처리
            // RottenMeatButton.gameObject.SetActive(false);
            // RottenWaterButton.gameObject.SetActive(false);

            SetButtons();
        }

        private void HaveMeat(MeatType meatType)
        {
            EnoughFullnessOrHidration(gageController.fullnessBar);
            gageController.GageUpdate(gageController.fullnessBar, meatIncreaseFullness);
            switch (meatType)
            {
                case MeatType.Fresh:
                    break;
                case MeatType.Suspicious:
                    diseaseController.GotDiseaseBySuspicious();
                    break;
                case MeatType.Rotten:
                    diseaseController.GotDiseaseByRotten(diseaseController.getDiseasePercentage);
                    break;
            }
        }

        private void HaveWater(WaterType waterType)
        {
            EnoughFullnessOrHidration(gageController.hidrationBar);
            gageController.GageUpdate(gageController.hidrationBar, waterIncreaseHidration);
            switch (waterType)
            {
                case WaterType.Fresh:
                    break;
                case WaterType.Rotten:
                    diseaseController.GotDiseaseByRotten(diseaseController.getDiseasePercentage);
                    break;
            }
        }

        public void EnoughFullnessOrHidration(Image bar)
        {
            if (bar.fillAmount >= gageController.maxAmount)
            {
                diseaseController.AddDiseaseStack(1);
            }
        }

        public void GaveBeastMeat()
        {

        }

        private void SetButtons()
        {
            freshMeatButton.onClick.AddListener(() => HaveMeat(MeatType.Fresh));
            suspiciousMeatButton.onClick.AddListener(() => HaveMeat(MeatType.Suspicious));
            rottenMeatButton.onClick.AddListener(() => HaveMeat(MeatType.Rotten));
            freshWaterButton.onClick.AddListener(() => HaveWater(WaterType.Fresh));
            rottenWaterButton.onClick.AddListener(() => HaveWater(WaterType.Rotten));
        }
    }
}