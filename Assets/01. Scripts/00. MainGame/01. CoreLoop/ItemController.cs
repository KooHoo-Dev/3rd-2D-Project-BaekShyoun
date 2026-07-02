using System.Collections;
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
        
        [Header("DesireSettings")]
        public float meatIncreaseFullness = 0.2f;
        public float waterIncreaseHidration = 0.1f;

        private GageController gageController;
        private DiseaseController diseaseController;

        private void Awake()
        {
            gageController = GetComponent<GageController>();
            diseaseController = GetComponent<DiseaseController>();
        }

        public void HaveMeat(MeatType meatType)
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

        public void HaveWater(WaterType waterType)
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
            int randNum = Random.Range(1, 101);
            if (bar.fillAmount >= gageController.maxAmount && randNum <= 50)
            {
                diseaseController.AddDiseaseStack(1);
            }
        }

        public void GaveBeastMeat(MeatType meatType)
        {
            switch(meatType)
            {
                case MeatType.Fresh:
                    break;
                case MeatType.Suspicious:
                    break;
                case MeatType.Rotten:
                    break;
            }
        }
    }
}