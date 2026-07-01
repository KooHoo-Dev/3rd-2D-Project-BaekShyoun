using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

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
        public bool isBeastInventory =false;


        private void Awake()
        {
            SetObjectsInactive();
            SetTextEmpty();
            uiInteraction = GetComponentInParent<UIInteraction>();
            itemController = GetComponentInParent<ItemController>();
            yesText.text = "네";
            noText.text = "아니오";
        }

        private void Start()
        {
            for (int i = 0; i < itemButtons.Length; ++i)
            {
                Button button = itemButtons[i];
                button.onClick.RemoveAllListeners();
                if(isBeastInventory)
                {
                    button.onClick.AddListener(() => SetButtonsForBeast(button));
                }
                else
                {
                    button.onClick.AddListener(() => SetDescriptionWhenClick(button));
                }
            }
        }

        #region 상태 관리
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
        #endregion

        #region ForPlayer
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
            switch (itemName)
            {
                case "신선한 고기": // 0 3 4 1 5
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
        }
        #endregion
        // SetDescriptionWhenClick > 기본적인 문구 바꾸고 버튼 텍스트 변경 이후 eatbutton에 연결(InteractEatButton)
        // InteractEatButton > eat 버튼이 눌렸을 시 팝업이 뜨고(SetEatPopUPText) 다시 팝업 상호작용(SetButtonEffect)으로 이동
        // SetButtonEffect > yes버튼을 누르게 될때 어떤 효과가 있을지 확인할 수 있다. (Beast의 경우 BeastController의 내용을 관리)

        #region ForBeast


        private void SetButtonsForBeast(Button button)
        {
            SetObjectsActive();
            SetTextEmpty();
            discardButton.gameObject.SetActive(false);
            string itemName = button.GetComponentInChildren<TextMeshProUGUI>().text;
            switch (itemName)
            {
                case "신선한 물" or "수면권" or "썩은 물":
                    description.text = "해당 아이템은 맹수에게 급여할 수 없습니다!";
                    eatButton.gameObject.SetActive(false);
                    break;
                case "신선한 고기" or "수상한 고기" or "부패한 고기":
                    eatButtonText.text = "먹이기";
                    SetFeedPopUpText(itemName);
                    eatButton.onClick.AddListener(uiInteraction.ControlEventScript);
                    break;
            }
        }
        private void SetFeedPopUpText(string itemName)
        {
            popUpText.text = $"{itemName}을(를) 맹수에게 건네겠습니까?";
        }

        private void SetBeastButtonEffect()
        {

        }
        #endregion
    }
}