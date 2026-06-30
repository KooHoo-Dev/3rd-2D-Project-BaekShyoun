using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

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

        [Header("ExchangeMachineSettings")]
        public int freshMeatPrice = 200;
        public int freshWaterPrice = 100;
        public int suspiciousMeatPrice = 50;
        public int sleepTicketPrice = 200;

        #region Rotten 전환

        #endregion

        private void BuyItems(int count, int price)
        {
            currentPoint -= price;
            count++;
        }

        public void BuyFreshMeat()
        {
            BuyItems(freshMeatCount, freshMeatPrice);
        }

        public void BuySuspiciousMeat()
        {
            BuyItems(suspiciousMeatCount, suspiciousMeatPrice);
        }

        public void BuyFreshWater()
        {
            BuyItems(freshWaterCount, freshWaterPrice);
        }

        public void BuySleepTicket()
        {
            BuyItems(sleepTicketCount, sleepTicketPrice);
        }
    }
}