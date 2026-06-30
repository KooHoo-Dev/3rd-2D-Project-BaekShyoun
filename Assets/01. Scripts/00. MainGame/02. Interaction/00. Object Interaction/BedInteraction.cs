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
        public TextMeshProUGUI yesText;
        public Button noButton;
        public TextMeshProUGUI noText;
        public TextMeshProUGUI eventScript;
        public TurnController turnController;

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
                    yesButton.onClick.AddListener(InteractYesButton);
                    noButton.onClick.RemoveAllListeners();
                    noButton.onClick.AddListener(InteractNoButton);
                    SetPopUpText();
                    uiInteraction.ControlPopUpScreen();
                }
            }
        }

        private void CheckPlayerPos()
        {
            if (this.name == "Bed(Under)")
                CheckPlayerState(CharacterMove.State.Back);
            if (this.name == "Bed(Side)")
                CheckPlayerState(CharacterMove.State.Right);
            if (this.name == "Bed(Upper)")
                CheckPlayerState(CharacterMove.State.Front);
        }

        private void SetPopUpText()
        {
            popUpText.text = "수면권을 사용하시겠습니까?";
            yesText.text = "네";
            noText.text = "아니오";
        }

        private void InteractYesButton()
        {
            uiInteraction.ActiveEventScriptScreen();
            eventScript.text = "수면권을 1개 사용하여 당신은 깊은 수면에 빠졌습니다!";
            turnController.OnDeepSleep();
        }

        private void InteractNoButton()
        {
            uiInteraction.ActiveEventScriptScreen();
            eventScript.text = "당신은 조금 불편한 잠을 청했습니다!";
            turnController.OnNormalSleep();
        }
    }
}
