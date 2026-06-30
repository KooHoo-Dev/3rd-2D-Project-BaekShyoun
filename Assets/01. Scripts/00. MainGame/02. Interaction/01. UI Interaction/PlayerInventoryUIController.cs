using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ObservedProject
{
    public class PlayerInventoryUIController : MonoBehaviour
    {
        public Button[] itemButtons;
        public Button eatButton;
        public Button discardButton;
        public TextMeshProUGUI description;
        public TextMeshProUGUI eatButtonText;
        public TextMeshProUGUI discardButtonText;
        public TextMeshProUGUI popUpText;
        public Button yesButton;
        public TextMeshProUGUI yesText;
        public Button noButton;
        public TextMeshProUGUI noText;
        public TextMeshProUGUI currentPoint;

        private UIInteraction uiInteraction;
        private ItemController itemController;
        private InventoryItemSystem inventoryItemSystem;

        private void Awake()
        {
            SetObjectsInactive();
            SetTextEmpty();
            uiInteraction = GetComponentInParent<UIInteraction>();
            itemController = GetComponentInParent<ItemController>();
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

        private void Update()
        {
            currentPoint.text = $"{inventoryItemSystem.currentPoint}";
        }

        private void SetObjectsActive()
        {
            eatButton.gameObject.SetActive(true);
            discardButton.gameObject.SetActive(true);
            description.gameObject.SetActive(true);
        }

        private void SetObjectsInactive()
        {
            eatButton.gameObject.SetActive(false);
            discardButton.gameObject.SetActive(false);
            description.gameObject.SetActive(false);
        }
        
        private void SetTextEmpty()
        {
            description.text = string.Empty;
            eatButtonText.text = string.Empty;
            discardButtonText.text = string.Empty;
            popUpText.text = string.Empty;
        }

        private void SetDescriptionWhenClick(Button button)
        {
            SetObjectsActive();
            SetTextEmpty();

            discardButtonText.text = "버리기";

            string itemName = button.GetComponentInChildren<TextMeshProUGUI>().text;

            switch(itemName)
            {
                case "신선한 고기":
                    eatButtonText.text = "먹기";
                    description.text = "먹기 좋아보이는 고기.\n상태는 신선해 보인다.";
                    InteractEatButton(itemName);
                    break;
                case "수상한 고기":
                    eatButtonText.text = "먹기";
                    description.text = "난생 처음 본 고기.\n맛은 좀 이상하지만 그래도 배는 찬다.";
                    InteractEatButton(itemName);
                    break;
                case "부패한 고기":
                    eatButtonText.text = "먹기";
                    description.text = "상온에 오래 있어 상한 고기.\n먹으면 속이 안 좋아질지도 모른다.";
                    InteractEatButton(itemName);
                    break;
                case "신선한 물":
                    eatButtonText.text = "마시기";
                    description.text = "마시기 좋은 물.\n목을 축일 수 있을 것 같다.";
                    InteractEatButton(itemName);
                    break;
                case "썩은 물":
                    eatButtonText.text = "마시기";
                    description.text = "상태가 좋아보이지 않는 물.\n마시면 속이 안 좋아질지도 모른다.";
                    InteractEatButton(itemName);
                    break;
                case "수면권":
                    eatButton.gameObject.SetActive(false);
                    description.text = "침대 옆 단말기에 사용하면\n조금 더 편히 잘 수 있을 것 같다.";
                    break;
            }

            InteractDiscardButton(itemName);
        }

        private void InteractEatButton(string itemName)
        {
            eatButton.onClick.AddListener(uiInteraction.ControlPopUpScreen);
            eatButton.onClick.AddListener(()=>SetEatPopUpText(itemName));
        }


        private void InteractDiscardButton(string itemName)
        {
            discardButton.onClick.AddListener(uiInteraction.ControlPopUpScreen);
            discardButton.onClick.AddListener(()=>SetDiscardPopUpText(itemName));
        }

        private void SetEatPopUpText(string itemName)
        {
            popUpText.text = $"아이템 {itemName}을(를) 사용하겠습니까?";
            SetButtonEffect(itemName);
        }

        private void SetDiscardPopUpText(string itemName)
        {
            popUpText.text = $"아이템 {itemName}을(를) 버리겠습니까?";
        }

        private void SetButtonEffect(string itemName)
        {
            yesButton.onClick.RemoveAllListeners();
            switch(itemName)
            {
                case "신선한 고기":
                    yesButton.onClick.AddListener(() => itemController.HaveMeat(ItemController.MeatType.Fresh));
                    break;
                case "수상한 고기":
                    yesButton.onClick.AddListener(() => itemController.HaveMeat(ItemController.MeatType.Suspicious));
                    break;
                case "부패한 고기":
                    yesButton.onClick.AddListener(() => itemController.HaveMeat(ItemController.MeatType.Rotten));
                    break;
                case "신선한 물":
                    yesButton.onClick.AddListener(() => itemController.HaveWater(ItemController.WaterType.Fresh));
                    break;
                case "썩은 물":
                    yesButton.onClick.AddListener(() => itemController.HaveWater(ItemController.WaterType.Rotten));
                    break;
            }
            yesButton.onClick.AddListener(() => uiInteraction.ScreenOff(uiInteraction.popUpScreen));
        }
    }
}