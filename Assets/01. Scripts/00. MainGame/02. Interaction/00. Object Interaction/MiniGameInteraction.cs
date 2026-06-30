using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace ObservedProject
{
    public class MiniGameInteraction : ObjectInteraction
    {
        public TextMeshProUGUI popUpText;
        public Button yesButton;
        public TextMeshProUGUI yesText;
        public Button noButton;
        public TextMeshProUGUI noText;
        public TurnController turnController;

        private void Update()
        {
            CheckPlayerState(CharacterMove.State.Back);
            if (ScoreManagerGameB.Instance != null && ScoreManagerGameB.Instance.IsEndGame)
            {
                ScoreManagerGameB.Instance.IsEndGame = false;
                turnController.OnPlayGame();
            }
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
                    uiInteraction.ControlPopUpScreen();
                    SetPopUpText();
                }
            }
        }

        private void SetPopUpText()
        {
            popUpText.text = "게임을 플레이하시겠습니까?";
            yesText.text = "네";
            noText.text = "아니오";
        }

        private void InteractYesButton()
        {
            if (this.name == "GameA") SceneManager.LoadScene(2, LoadSceneMode.Additive);
            if (this.name == "GameB") SceneManager.LoadScene(3, LoadSceneMode.Additive);
        }
    }
}