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
        public InventoryItemSystem inventoryItemSystem;
        public int getPoint;

        public GageController gageController;
        public GameObject mainGameUI;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            CheckPlayerState(CharacterMove.State.Back);
            if (ScoreManagerGameA.Instance != null && ScoreManagerGameA.Instance.isEndGame)
            {
                ScoreManagerGameA.Instance.isEndGame = false;
                characterMove.canMove = true;
                mainGameUI.SetActive(true);
                turnController.OnPlayGame();
                getPoint = ScoreManagerGameA.Instance.finalScore;
                inventoryItemSystem.currentPoint += getPoint;
                inventoryItemSystem.currentPointText.text = $"{inventoryItemSystem.currentPoint} pt";
                ScoreManagerGameA.Instance.DestroyManager();
            }
            if (ScoreManagerGameB.Instance != null && ScoreManagerGameB.Instance.isEndGame)
            {
                ScoreManagerGameB.Instance.isEndGame = false;
                characterMove.canMove = true;
                mainGameUI.SetActive(true);
                turnController.OnPlayGame();
                getPoint = ScoreManagerGameB.Instance.finalScore;
                inventoryItemSystem.currentPoint += getPoint;
                inventoryItemSystem.currentPointText.text = $"{inventoryItemSystem.currentPoint} pt";
                ScoreManagerGameB.Instance.DestroyManager();
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

        // 씬 불러오는 동안 메인 씬 ui 상호작용 전부 작동하지 않게 하는 방법 생각해보기 (나중에)
        private void InteractYesButton()
        {
            characterMove.canMove = false;
            mainGameUI.SetActive(false);
            if (this.name == "GameA") SceneManager.LoadScene(2, LoadSceneMode.Additive);
            if (this.name == "GameB") SceneManager.LoadScene(3, LoadSceneMode.Additive);
        }
    }
}