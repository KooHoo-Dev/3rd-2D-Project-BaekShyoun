using TMPro;
using UnityEngine;

namespace ObservedProject
{
    public class DiseaseController : MonoBehaviour
    {
        [Header("DiseaseSettings")]
        public int getDiseasePercentage = 30;
        public int removeDiseasePercentage = 50;
        public float diseaseDebuff = 1.2f;
        public TextMeshProUGUI diseaseStackText;

        private int diseaseStack;
        private int suspiciousCount;

        private void Awake()
        {
            // 질병 스택 0으로 초기화
            diseaseStack = 0;
            suspiciousCount = 0;
            SetText();
        }

        public void GotDiseaseByRotten(int percentage)
        {
            int number = Random.Range(1, 101);
            if (number <= percentage) AddDiseaseStack(1);
            Debug.Log($"Number : {number}");
        }

        public void GotDiseaseBySuspicious()
        {
            suspiciousCount++;
            if (suspiciousCount >= 3)
            {
                AddDiseaseStack(1);
                Debug.Log("수상한 고기 3회 섭취로 질병 스택 +1 !");
                suspiciousCount = 0;
            }
        }

        public void AddDiseaseStack(int amount)
        {
            diseaseStack += amount;
            SetText();
        }

        public float ChangeStatIfSick(float amount)
        {
            if (diseaseStack > 0 && amount < 0)
            {
                float actualDebuff = Mathf.Pow(diseaseDebuff, diseaseStack);
                amount *= actualDebuff;
            }
            return amount;
        }

        public void RemoveDiseaseStack()
        {
            int number = Random.Range(1, 101);
            if (number <= removeDiseasePercentage) diseaseStack--;
            Debug.Log($"Disease Remove number : {number}");
            if (diseaseStack < 0) diseaseStack = 0;
            SetText();
        }

        public void SetText()
        {
            diseaseStackText.text = $"질병 {diseaseStack} 중첩";
        }
    }
}