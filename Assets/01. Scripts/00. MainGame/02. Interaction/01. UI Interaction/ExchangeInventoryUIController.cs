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
        public TextMeshProUGUI noText;
        public TextMeshProUGUI priceText;

        private UIInteraction uiInteraction;
        private InventoryItemSystem inventoryItemSystem;

        private void Awake()
        {
            SetObjectsInactive();
            SetTextEmpty();
            uiInteraction = GetComponentInParent<UIInteraction>();
            inventoryItemSystem = GetComponentInParent<InventoryItemSystem>();
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
                    SetPopUpText("신선한 고기");
                    yesButton.onClick.AddListener(inventoryItemSystem.BuyFreshMeat);
                    break;
                case "SuspiciousMeat":
                    description.text = "<b>수상한 고기</b>\n난생 처음 본 고기.\n맛은 좀 이상하지만 그래도 배는 찬다.";
                    priceText.text = $"{inventoryItemSystem.suspiciousMeatPrice} pt";
                    SetPopUpText("수상한 고기");
                    yesButton.onClick.AddListener(inventoryItemSystem.BuySuspiciousMeat);
                    break;
                case "FreshWater":
                    description.text = "<b>신선한 물</b>\n마시기 좋은 물.\n목을 축일 수 있을 것 같다.";
                    priceText.text = $"{inventoryItemSystem.freshWaterPrice} pt";
                    SetPopUpText("신선한 물");
                    yesButton.onClick.AddListener(inventoryItemSystem.BuyFreshWater);
                    break;
                case "SleepTicket":
                    description.text = "<b>수면권</b>\n침대 옆 단말기에 사용하면\n조금 더 편히 잘 수 있을 것 같다.";
                    priceText.text = $"{inventoryItemSystem.sleepTicketPrice} pt";
                    SetPopUpText("수면권");
                    yesButton.onClick.AddListener(inventoryItemSystem.BuySleepTicket);
                    break;
            }
            yesButton.onClick.AddListener(() => uiInteraction.ScreenOff(uiInteraction.popUpScreen));
        }

        private void SetPopUpText(string text)
        {
            popUpText.text = $"{text}을(를) 구매하시겠습니까?";
        }
        
    }
}