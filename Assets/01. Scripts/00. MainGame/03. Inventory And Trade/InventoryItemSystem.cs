using UnityEngine;
using TMPro;
using Unity.VisualScripting;

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
        public TextMeshProUGUI eventScriptText;

        [Header("ExchangeMachineSettings")]
        public int freshMeatPrice = 200;
        public int freshWaterPrice = 100;
        public int suspiciousMeatPrice = 50;
        public int sleepTicketPrice = 200;

        public int[] counts { get; private set; }
        private UIInteraction uiInteraction;
        private ExchangeInventoryUIController exchangeInventory;

        private void Awake()
        {
            uiInteraction = GetComponentInParent<UIInteraction>();
            exchangeInventory = GetComponentInChildren<ExchangeInventoryUIController>(true);
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

        //#region Rotten 전환



        //#endregion


        #region 교환 자판기 이용

        private void BuyItems(int playerCount, int price, int countIndex)
        {
            while(true)
            {
                if (currentPoint < price)
                {
                    uiInteraction.ControlEventScript();
                    eventScriptText.text = "보유하고 있는 포인트가 부족합니다.";
                    break;
                }
                for (int i = 0; i < exchangeInventory.exchangeCounts.Length; ++i)
                {
                    if (exchangeInventory.exchangeCounts[i] <= 0)
                    {
                        return;
                    }
                }
                playerCount++;
                SetBoughtCountText(playerCount, countIndex);
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


        #region 개수 관리
        
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

        public bool DecreaseItem(int countIndex)
        {
            eventScriptText.text = string.Empty;
            switch (countIndex)
            {
                case 0:
                    if (freshMeatCount <= 0)
                    {
                        SetEventScript();
                        return false;
                    }
                    freshMeatCount--;
                    countTexts[countIndex].text = $"{freshMeatCount}";
                    return true;
                case 1:
                    if (freshWaterCount <= 0)
                    {
                        SetEventScript();
                        return false;
                    }
                    freshWaterCount--;
                    countTexts[countIndex].text = $"{freshWaterCount}";
                    return true;
                case 2:
                    if (sleepTicketCount <= 0)
                    {
                        SetEventScript();
                        return false;
                    }
                    sleepTicketCount--;
                    countTexts[countIndex].text = $"{sleepTicketCount}";
                    return true;
                case 3:
                    if (suspiciousMeatCount <= 0)
                    {
                        SetEventScript();
                        return false;
                    }
                    suspiciousMeatCount--;
                    countTexts[countIndex].text = $"{suspiciousMeatCount}";
                    return true;
                case 4:
                    if (rottenMeatCount <= 0)
                    {
                        SetEventScript();
                        return false;
                    }
                    rottenMeatCount--;
                    countTexts[countIndex].text = $"{rottenMeatCount}";
                    return true;
                case 5:
                    if (rottenWaterCount <= 0)
                    {
                        SetEventScript();
                        return false;
                    }
                    rottenWaterCount--;
                    countTexts[countIndex].text = $"{rottenWaterCount}";
                    return true;
            }
            return false;
        }

        private void SetEventScript()
        {
            uiInteraction.ControlEventScript();
            eventScriptText.text = "아이템이 존재하지 않습니다.";
        }

        #endregion
    }
}