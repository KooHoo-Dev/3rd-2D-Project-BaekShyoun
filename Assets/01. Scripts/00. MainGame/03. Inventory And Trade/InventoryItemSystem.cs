using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Constraints;
using UnityEditor;

namespace ObservedProject
{
    public class InventoryItemSystem : MonoBehaviour
    {
        [Header("PlayerSettings")]
        public int currentPoint = 1000;
        public int freshMeatCount = 0;
        public int suspiciousMeatCount = 0;
        public int rottenMeatCount = 0;
        public int freshWaterCount = 0;
        public int rottenWaterCount = 0;
        public int sleepTicketCount = 0;
        public TextMeshProUGUI[] countTexts;
        public TextMeshProUGUI currentPointText;

        [Header("ExchangeMachineSettings")]
        public int freshMeatPrice = 200;
        public int freshWaterPrice = 100;
        public int suspiciousMeatPrice = 50;
        public int sleepTicketPrice = 200;

        public int[] counts { get; private set; }
        private UIInteraction uiInteraction;

        private void Awake()
        {
            uiInteraction = GetComponentInParent<UIInteraction>();
            currentPoint = 1000;
            currentPointText.text = $"{currentPoint} pt";
            for (int i = 0; i < countTexts.Length; ++i)
            {
                SetBoughtCountText(0, i);
            }
            counts = new int[6] { freshMeatCount, suspiciousMeatCount, rottenMeatCount, freshWaterCount, rottenWaterCount, sleepTicketCount };
            for (int i = 0; i < counts.Length; ++i)
            {
                if (counts[i] < 0)
                {
                    counts[i] = 0;
                    uiInteraction.ControlEventScript();
                }
            }
        }

        #region Rotten 전환



        #endregion


        #region 교환 자판기 이용

        private void BuyItems(int count, int price, int countIndex)
        {
            while(true)
            {
                if (currentPoint < price)
                {
                    uiInteraction.ControlEventScript();
                    break;
                }
                count++;
                SetBoughtCountText(count, countIndex);
                currentPoint -= price;
                currentPointText.text = $"{currentPoint} pt";
                return;
            }
            
        }

        public void BuyFreshMeat()
        {
            BuyItems(freshMeatCount, freshMeatPrice, 0);
            
        }

        public void BuySuspiciousMeat() //3
        {
            BuyItems(suspiciousMeatCount, suspiciousMeatPrice, 3);
        }

        public void BuyFreshWater() //1
        {
            BuyItems(freshWaterCount, freshWaterPrice, 1);
        }

        public void BuySleepTicket() //2
        {
            BuyItems(sleepTicketCount, sleepTicketPrice, 2);
        }
        #endregion

        private void SetBoughtCountText(int count, int countIndex)
        {
            switch(countIndex)
            {
                case 0:
                    freshMeatCount = count;
                    countTexts[countIndex].text = $"{count}";
                    break;
                case 1:
                    freshWaterCount = count;
                    countTexts[countIndex].text = $"{count}";
                    break;
                case 2:
                    sleepTicketCount = count;
                    countTexts[countIndex].text = $"{count}";
                    break;
                case 3:
                    suspiciousMeatCount = count;
                    countTexts[countIndex].text = $"{count}";
                    break;
                case 4:
                    rottenMeatCount = count;
                    countTexts[countIndex].text = $"{count}";
                    break;
                case 5:
                    rottenWaterCount = count;
                    countTexts[countIndex].text = $"{count}";
                    break;
            }
        }
    }
}