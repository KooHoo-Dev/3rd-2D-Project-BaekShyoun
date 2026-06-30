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
        private ScoreManagerGameB scoreB;
        public int point;

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
            turnController.OnPlayGame();
            // 이렇게 하면 다 좋은데 게임 끝나고 턴 체크가 표시가 안됨...
            // tab키(스테이터스 창)로 보완 가능하긴 하지만 추후 다시 와서 고민해보면 좋을듯
        }

        private int GetScore()
        {
            point += scoreB.finalScore;
            return point;
        }
    }
}