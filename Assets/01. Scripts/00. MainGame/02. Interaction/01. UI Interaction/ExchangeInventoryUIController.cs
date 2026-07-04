using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ObservedProject
{
    public class ExchangeInventoryUIController : MonoBehaviour
    {
        public Button[] itemButtons;
        public Button purchaseButton;
        public TextMeshProUGUI description;
        public TextMeshProUGUI popUpText;
        public Button yesButton;
        public TextMeshProUGUI yesText;
        public Button noButton;
        public TextMeshProUGUI eventScriptText;
        public TextMeshProUGUI noText;
        public TextMeshProUGUI priceText;
        public TextMeshProUGUI itemCountText;
        public int[] exchangeCounts { get; private set; }

        private UIInteraction uiInteraction;
        private InventoryItemSystem inventoryItemSystem;

        private void Awake()
        {
            SetObjectsInactive();
            uiInteraction = GetComponentInParent<UIInteraction>();
            inventoryItemSystem = GetComponentInParent<InventoryItemSystem>();
            exchangeCounts = new int[4] { 5, 5, 5, 5 };
            yesText.text = "네";
            noText.text = "아니오";
        }

        private void Start()
        {
            for (int i = 0; i < itemButtons.Length; ++i)
            {
                Button button = itemButtons[i];
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => SetDescriptionWhenClick(button));
            }
        }

        private void SetObjectsActive()
        {
            purchaseButton.gameObject.SetActive(true);
            description.gameObject.SetActive(true);
        }

        private void SetObjectsInactive()
        {
            purchaseButton.gameObject.SetActive(false);
            description.gameObject.SetActive(false);
        }

        private void SetTextEmpty()
        {
            description.text = string.Empty;
            popUpText.text = string.Empty;
            priceText.text = string.Empty;
            
        }

        private void SetDescriptionWhenClick(Button button)
        {
            SetObjectsActive();
            SetTextEmpty();

            string itemName = button.name;

            yesButton.onClick.RemoveAllListeners();
            switch (itemName)
            {
                case "FreshMeat":
                    description.text = "<b>신선한 고기</b>\n먹기 좋아보이는 고기.\n상태는 신선해 보인다.";
                    priceText.text = $"{inventoryItemSystem.freshMeatPrice} pt";
                    itemCountText.text = $"개수 : {exchangeCounts[0]}";
                    SetPopUpText("신선한 고기");
                    yesButton.onClick.AddListener(() =>
                    {
                        if (exchangeCounts[0] <= 0)
                        {
                            SetEventScript();
                            return;
                        }

                        if (inventoryItemSystem.BuyFreshMeat())
                        {
                            DecreaseItem(0);
                        }
                    });
                    break;
                case "SuspiciousMeat":
                    description.text = "<b>수상한 고기</b>\n난생 처음 본 고기.\n맛은 좀 이상하지만 그래도 배는 찬다.";
                    priceText.text = $"{inventoryItemSystem.suspiciousMeatPrice} pt";
                    itemCountText.text = $"개수 : {exchangeCounts[1]}";
                    SetPopUpText("수상한 고기");
                    yesButton.onClick.AddListener(() =>
                    {
                        if (exchangeCounts[1] <= 0)
                        {
                            SetEventScript();
                            return;
                        }

                        if (inventoryItemSystem.BuySuspiciousMeat())
                        {
                            DecreaseItem(1);
                        }
                    });
                    break;
                case "FreshWater":
                    description.text = "<b>신선한 물</b>\n마시기 좋은 물.\n목을 축일 수 있을 것 같다.";
                    priceText.text = $"{inventoryItemSystem.freshWaterPrice} pt";
                    itemCountText.text = $"개수 : {exchangeCounts[2]}";
                    SetPopUpText("신선한 물");
                    yesButton.onClick.AddListener(() =>
                    {
                        if (exchangeCounts[2] <= 0)
                        {
                            SetEventScript();
                            return;
                        }

                        if (inventoryItemSystem.BuyFreshWater())
                        {
                            DecreaseItem(2);
                        }
                    });
                    break;
                case "SleepTicket":
                    description.text = "<b>수면권</b>\n침대 옆 단말기에 사용하면\n조금 더 편히 잘 수 있을 것 같다.";
                    priceText.text = $"{inventoryItemSystem.sleepTicketPrice} pt";
                    itemCountText.text = $"개수 : {exchangeCounts[3]}";
                    SetPopUpText("수면권");
                    yesButton.onClick.AddListener(() =>
                    {
                        if (exchangeCounts[3] <= 0)
                        {
                            SetEventScript();
                            return;
                        }

                        if (inventoryItemSystem.BuySleepTicket())
                        {
                            DecreaseItem(3);
                        }
                    });
                    break;
            }
            yesButton.onClick.AddListener(() => uiInteraction.ScreenOff(uiInteraction.popUpScreen));
        }

        private void SetPopUpText(string text)
        {
            popUpText.text = $"{text}을(를) 구매하시겠습니까?";
        }

        public void DecreaseItem(int index)
        {
            eventScriptText.text = string.Empty;

            
            exchangeCounts[index]--;
            if (exchangeCounts[index] <= 0)
            {
                exchangeCounts[index] = 0;
            }
            itemCountText.text = $"개수 : {exchangeCounts[index]}";
        
        }

        private void SetEventScript()
        {
            uiInteraction.ControlEventScript();
            eventScriptText.text = "더 이상 구매가 불가능합니다.";
        }

    }
}