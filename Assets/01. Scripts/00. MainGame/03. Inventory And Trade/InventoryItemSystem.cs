using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace ObservedProject
{
    public class InventoryItemSystem : MonoBehaviour
    {
        [Header("PlayerSettings")]
        public int currentPoint = 1000;
        public int rottenMeatCount = 0;
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
        private TurnController turnController;
        private UIInteraction uiInteraction;

        #region Rotten 관련

        public List<ConsumablesData> freshMeatList;
        public List<ConsumablesData> suspiciousMeatList;
        public List<ConsumablesData> freshWaterList;

        #endregion

        private void Awake()
        {
            turnController = GetComponentInParent<TurnController>();
            uiInteraction = GetComponentInParent<UIInteraction>();
            currentPoint = 1000;
            currentPointText.text = $"{currentPoint} pt";

            freshMeatList = new List<ConsumablesData>();
            suspiciousMeatList = new List<ConsumablesData>();
            freshWaterList = new List<ConsumablesData>();

            for (int i = 0; i < countTexts.Length; ++i)
            {
                SetBoughtCountText(0, i);
            }
            counts = new int[6] { freshMeatList.Count, freshWaterList.Count, sleepTicketCount, suspiciousMeatList.Count, rottenMeatCount,  rottenWaterCount };
            for (int i = 0; i < counts.Length; ++i)
            {
                if (counts[i] < 0)
                {
                    counts[i] = 0;
                    uiInteraction.ControlEventScript();
                }
            }
        }

        private void AddFreshMeat()
        {
            ConsumablesData freshMeat = new ConsumablesData();
            freshMeat.registeredTurn = turnController.turnCount;

            freshMeatList.Add(freshMeat);

            counts[0] = freshMeatList.Count;
            SetBoughtCountText(counts[0], 0);
        }

        private void AddFreshWater()
        {
            ConsumablesData water = new ConsumablesData();
            water.registeredTurn = turnController.turnCount;

            freshWaterList.Add(water);

            counts[1] = freshWaterList.Count;
            SetBoughtCountText(counts[1], 1);
        }

        private void AddSuspiciousMeat()
        {
            ConsumablesData meat = new ConsumablesData();
            meat.registeredTurn = turnController.turnCount;

            suspiciousMeatList.Add(meat);

            counts[3] = suspiciousMeatList.Count;
            SetBoughtCountText(counts[3], 3);
        }

        #region 교환 자판기 이용

        private bool BuyItems(int price)
        {
            if (currentPoint < price)
            {
                uiInteraction.ControlEventScript();
                eventScriptText.text = "보유하고 있는 포인트가 부족합니다.";
                return false;
            }
            currentPoint -= price;
            currentPointText.text = $"{currentPoint} pt";
            return true;

            
        }

        public bool BuyFreshMeat()
        {
            if (BuyItems(freshMeatPrice))
            {
                AddFreshMeat();
                return true;
            }

            return false;
        }

        public bool BuyFreshWater()
        {
            if (BuyItems(freshWaterPrice))
            {
                AddFreshWater();
                return true;
            }

            return false;
        }

        public bool BuySleepTicket()
        {
            if (BuyItems(sleepTicketPrice))
            {
                sleepTicketCount++;
                counts[2] = sleepTicketCount;
                SetBoughtCountText(counts[2], 2);
                return true;
            }
            return false;
        }

        public bool BuySuspiciousMeat()
        {
            if (BuyItems(suspiciousMeatPrice))
            {
                AddSuspiciousMeat();
                return true;
            }

            return false;
        }

        #endregion


        #region 개수 관리
        
        private void SetBoughtCountText(int count, int countIndex)
        {
            countTexts[countIndex].text = $"{count}";
        }

        public bool DecreaseItem(int countIndex)
        {
            eventScriptText.text = string.Empty;
            switch (countIndex)
            {
                case 0:
                    if (freshMeatList.Count <= 0)
                    {
                        SetEventScript();
                        return false;
                    }

                    freshMeatList.RemoveAt(0);
                    counts[0] = freshMeatList.Count;
                    SetBoughtCountText(counts[0], 0);
                    return true;

                case 1:
                    if (freshWaterList.Count <= 0)
                    {
                        SetEventScript();
                        return false;
                    }

                    freshWaterList.RemoveAt(0);
                    counts[1] = freshWaterList.Count;
                    SetBoughtCountText(counts[1], 1);
                    return true;

                case 2:
                    if (sleepTicketCount <= 0)
                    {
                        SetEventScript();
                        return false;
                    }

                    sleepTicketCount--;
                    counts[2] = sleepTicketCount;
                    SetBoughtCountText(counts[2], 2);
                    return true;

                case 3:
                    if (suspiciousMeatList.Count <= 0)
                    {
                        SetEventScript();
                        return false;
                    }

                    suspiciousMeatList.RemoveAt(0);
                    counts[3] = suspiciousMeatList.Count;
                    SetBoughtCountText(counts[3], 3);
                    return true;

                case 4:
                    if (rottenMeatCount <= 0)
                    {
                        SetEventScript();
                        return false;
                    }

                    rottenMeatCount--;
                    counts[4] = rottenMeatCount;
                    SetBoughtCountText(counts[4], 4);
                    return true;

                case 5:
                    if (rottenWaterCount <= 0)
                    {
                        SetEventScript();
                        return false;
                    }

                    rottenWaterCount--;
                    counts[5] = rottenWaterCount;
                    SetBoughtCountText(counts[5], 5);
                    return true;
            }

            return false;
        }

        public void CheckRottenItems()
        {
            for (int i = freshMeatList.Count - 1; i >= 0; i--)
            {
                int passedTurn = turnController.turnCount - freshMeatList[i].registeredTurn;

                if (passedTurn >= 2)
                {
                    freshMeatList.RemoveAt(i);
                    rottenMeatCount++;
                }
            }

            for (int i = suspiciousMeatList.Count - 1; i >= 0; i--)
            {
                int passedTurn = turnController.turnCount - suspiciousMeatList[i].registeredTurn;

                if (passedTurn >= 2)
                {
                    suspiciousMeatList.RemoveAt(i);
                    rottenMeatCount++;
                }
            }

            for (int i = freshWaterList.Count - 1; i >= 0; i--)
            {
                int passedTurn = turnController.turnCount - freshWaterList[i].registeredTurn;

                if (passedTurn >= 4)
                {
                    freshWaterList.RemoveAt(i);
                    rottenWaterCount++;
                }
            }

            counts[0] = freshMeatList.Count;
            counts[1] = freshWaterList.Count;
            counts[3] = suspiciousMeatList.Count;
            counts[4] = rottenMeatCount;
            counts[5] = rottenWaterCount;

            SetBoughtCountText(counts[0], 0);
            SetBoughtCountText(counts[1], 1);
            SetBoughtCountText(counts[3], 3);
            SetBoughtCountText(counts[4], 4);
            SetBoughtCountText(counts[5], 5);
        }

        private void SetEventScript()
        {
            uiInteraction.ControlEventScript();
            eventScriptText.text = "아이템이 존재하지 않습니다.";
        }

        #endregion
    }
}