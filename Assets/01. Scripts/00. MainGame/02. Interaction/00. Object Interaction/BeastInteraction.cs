using Study.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace ObservedProject
{
    public class BeastInteraction : ObjectInteraction
    {
        public BeastController beastController;
        public TextMeshProUGUI popUpText;
        public Button yesButton;
        private TextMeshProUGUI yesText;
        public Button noButton;
        private TextMeshProUGUI noText;

        public GameObject inventory;
        private PlayerInventoryUIController playerInventory;

        protected override void Awake()
        {
            base.Awake();
            beastController = GetComponentInParent<BeastController>();
            playerInventory = inventory.GetComponentInChildren<PlayerInventoryUIController>(true);
            yesText = yesButton.GetComponentInChildren<TextMeshProUGUI>();
            noText = noButton.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            CheckPlayerState(CharacterMove.State.Back);
        }

        protected override void CheckPlayerState(CharacterMove.State state)
        {
            if (characterMove.currentState == state && canInteract)
            {
                if (SimpleInput.GetKeyDown(Key.F))
                {
                    yesButton.onClick.RemoveAllListeners();
                    yesButton.onClick.AddListener(InteractYesButton);
                    noButton.onClick.RemoveAllListeners();
                    SetPopUpText();
                    uiInteraction.ControlPopUpScreen();
                }
            }
        }
        private void SetPopUpText()
        {
            popUpText.text = "짐승에게 먹이를 주겠습니까?";
            yesText.text = "네";
            noText.text = "아니오";
        }

        private void InteractYesButton()
        {
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);

            uiInteraction.ClosePopUp();
            playerInventory.isBeastInventory = true;
            uiInteraction.ActivePlayerInventoryInForce();
            characterMove.canMove = false;
        }
    }
}