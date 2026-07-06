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

        private UIInteraction uiInteraction;
        private ItemController itemController;
        private InventoryItemSystem inventoryItemSystem;
        public BeastController beastController;
        public bool isBeastInventory =false;


        private void Awake()
        {
            SetObjectsInactive();
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
                button.onClick.AddListener(() =>
                {
                    if (isBeastInventory)
                        SetButtonsForBeast(button);
                    else
                        SetDescriptionWhenClick(button);
                });
            }
        }

        #region 오브젝트 상태 관리
        private void SetObjectsActive()
        {
            eatButton.gameObject.SetActive(true);
            discardButton.gameObject.SetActive(true);
            description.gameObject.SetActive(true);
        }

        public void SetObjectsInactive()
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
        #endregion

        #region ForPlayer
        private void SetDescriptionWhenClick(Button button)
        {
            eatButton.onClick.RemoveAllListeners();
            discardButton.onClick.RemoveAllListeners();

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
            eatButton.onClick.RemoveAllListeners();
            eatButton.onClick.AddListener(uiInteraction.ControlPopUpScreen);
            eatButton.onClick.AddListener(()=>SetEatPopUpText(itemName));
        }

        private void InteractDiscardButton(string itemName)
        {
            discardButton.onClick.RemoveAllListeners();
            discardButton.onClick.AddListener(uiInteraction.ControlPopUpScreen);
            discardButton.onClick.AddListener(()=>SetDiscardPopUpText(itemName));
        }

        private void SetEatPopUpText(string itemName)
        {
            popUpText.text = $"아이템 {itemName}을(를) 사용하겠습니까?";
            SetEatButtonEffect(itemName);
        }

        private void SetDiscardPopUpText(string itemName)
        {
            popUpText.text = $"아이템 {itemName}을(를) 버리겠습니까?";
            SetDiscardButtonEffect(itemName);
        }

        private void SetEatButtonEffect(string itemName)
        {
            yesButton.onClick.RemoveAllListeners();
            switch (itemName)
            {
                case "신선한 고기": // 0 3 4 1 5
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        if (inventoryItemSystem.DecreaseItem(0))
                        {
                            itemController.HaveMeat(ItemController.MeatType.Fresh);
                        }
                    });
                    break;
                case "수상한 고기":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        if (inventoryItemSystem.DecreaseItem(3))
                        {
                            itemController.HaveMeat(ItemController.MeatType.Suspicious);
                        }
                    });
                    break;
                case "부패한 고기":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        if (inventoryItemSystem.DecreaseItem(4))
                        {
                            itemController.HaveMeat(ItemController.MeatType.Rotten);
                        }
                    });
                    break;
                case "신선한 물":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        if (inventoryItemSystem.DecreaseItem(1))
                        {
                            itemController.HaveWater(ItemController.WaterType.Fresh);
                        }
                    });
                    break;
                case "썩은 물":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        if (inventoryItemSystem.DecreaseItem(5))
                        {
                            itemController.HaveWater(ItemController.WaterType.Rotten);
                        }
                    });
                    break;
            }
            yesButton.onClick.AddListener(uiInteraction.ClosePopUp);
        }

        private void SetDiscardButtonEffect(string itemName)
        {
            yesButton.onClick.RemoveAllListeners();
            switch (itemName)
            {
                case "신선한 고기":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        inventoryItemSystem.DecreaseItem(0);
                    });
                    break;
                case "수상한 고기":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        inventoryItemSystem.DecreaseItem(3);
                    });
                    break;
                case "부패한 고기":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        inventoryItemSystem.DecreaseItem(4);
                    });
                    break;
                case "신선한 물":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        inventoryItemSystem.DecreaseItem(1);
                    });
                    break;
                case "썩은 물":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        inventoryItemSystem.DecreaseItem(5);
                    });
                    break;
                case "수면권":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        inventoryItemSystem.DecreaseItem(2);
                    });
                    break;
            }
            yesButton.onClick.AddListener(uiInteraction.ClosePopUp);
        }
        #endregion

        #region ForBeast

        private void SetButtonsForBeast(Button button)
        {
            eatButton.onClick.RemoveAllListeners();
            discardButton.onClick.RemoveAllListeners();

            SetObjectsActive();
            SetTextEmpty();
            discardButton.gameObject.SetActive(false);
            string itemName = button.GetComponentInChildren<TextMeshProUGUI>().text;
            switch (itemName)
            {
                case "신선한 물" or "수면권" or "썩은 물":
                    description.text = "해당 아이템은 맹수에게 급여할 수 없다.";
                    eatButton.gameObject.SetActive(false);
                    break;
                case "신선한 고기":
                    eatButtonText.text = "먹이기";
                    description.text = "맹수가 가장 좋아할 것 같은 고기다.";
                    InteractFeedButton(itemName);
                    break;
                case "수상한 고기" or "부패한 고기":
                    eatButtonText.text = "먹이기";
                    description.text = "줘서 좋을 게 없을 듯하지만\n급한 불은 끌 수 있을 것 같다.";
                    InteractFeedButton(itemName);
                    break;
            }
        }

        private void InteractFeedButton(string itemName)
        {
            eatButton.onClick.RemoveAllListeners();
            eatButton.onClick.AddListener(uiInteraction.ControlPopUpScreen);
            eatButton.onClick.AddListener(() => SetFeedPopUpText(itemName));

        }
        private void SetFeedPopUpText(string itemName)
        {
            popUpText.text = $"{itemName}을(를) 맹수에게 건네겠습니까?";
            SetBeastButtonEffect(itemName);
        }

        private void SetBeastButtonEffect(string itemName)
        {
            yesButton.onClick.RemoveAllListeners();
            Debug.Log($"itemName: {itemName}");
            Debug.Log($"beastController: {beastController}");
            switch (itemName)
            {
                case "신선한 고기":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        if (inventoryItemSystem.DecreaseItem(0))
                        {
                            beastController.FeedFreshMeat();
                        }
                    });
                    break;
                case "수상한 고기":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        if (inventoryItemSystem.DecreaseItem(3))
                        {
                            beastController.FeedOtherMeat();
                        }
                    });
                    break;
                case "부패한 고기":
                    yesButton.onClick.AddListener(() =>
                    {
                        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
                        if (inventoryItemSystem.DecreaseItem(4))
                        {
                            beastController.FeedOtherMeat();
                        }
                    });
                    break;
            }
            yesButton.onClick.AddListener(uiInteraction.ClosePopUp);
        }
        #endregion
    }
}