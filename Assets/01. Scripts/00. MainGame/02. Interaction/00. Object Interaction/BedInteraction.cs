using Study.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace ObservedProject
{
    public class BedInteraction : ObjectInteraction
    {
        public TextMeshProUGUI popUpText;
        public Button yesButton;
        private TextMeshProUGUI yesText;
        public Button noButton;
        private TextMeshProUGUI noText;
        public TextMeshProUGUI eventScript;
        public TurnController turnController;
        public InventoryItemSystem inventoryItemSystem;

        protected override void Awake()
        {
            base.Awake();
            yesText = yesButton.GetComponentInChildren<TextMeshProUGUI>();
            noText = noButton.GetComponentInChildren<TextMeshProUGUI>();
            yesText.text = "네";
            noText.text = "아니오";
        }

        private void Update()
        {
            CheckPlayerPos();
        }

        protected override void CheckPlayerState(CharacterMove.State state)
        {
            if (characterMove.currentState == state && canInteract)
            {
                if (SimpleInput.GetKeyDown(Key.F))
                {
                    yesButton.onClick.RemoveAllListeners();
                    yesButton.onClick.AddListener(InteractFirstYesButton);
                    noButton.onClick.RemoveAllListeners();
                    noButton.onClick.AddListener(uiInteraction.ClosePopUp);
                    SetFirstPopUpText();
                    uiInteraction.OpenPopUp();
                }
            }
        }

        private void CheckPlayerPos()
        {
            if (this.name == "Bed(Under)")
                CheckPlayerState(CharacterMove.State.Back);
            if (this.name == "Bed(Side)")
            {
                CheckPlayerState(CharacterMove.State.Right);
                CheckPlayerState(CharacterMove.State.Back);
            }
            if (this.name == "Bed(Upper)")
                CheckPlayerState(CharacterMove.State.Front);
        }

        private void SetFirstPopUpText()
        {
            popUpText.text = "잠을 청하시겠습니까?";
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(InteractFirstYesButton);
        }

        private void InteractFirstYesButton()
        {
            SetSecondPopUpText();
        }

        private void SetSecondPopUpText()
        {
            popUpText.text = "수면권을 사용하시겠습니까?";
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(InteractSecondYesButton);
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(InteractSecondNoButton);
        }

        private void InteractSecondYesButton()
        {
            uiInteraction.ClosePopUp();
            
            if(inventoryItemSystem.DecreaseItem(2))
            {
                turnController.OnDeepSleep();
            }
        }

        private void InteractSecondNoButton()
        {
            uiInteraction.ClosePopUp();
            turnController.OnNormalSleep();
        }
    }
}
