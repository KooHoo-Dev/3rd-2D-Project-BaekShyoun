using NUnit.Framework.Constraints;
using Study.Utilities;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ObservedProject
{
    public class UIInteraction : MonoBehaviour
    {
        [Header("UI Screen")]
        public GameObject statusScreen;
        public GameObject eventScriptScreen;
        public GameObject turnCheckScreen;
        public GameObject playerInventoryScreen;
        public GameObject exchangeInventoryScreen;
        public GameObject popUpScreen;

        [Header("Pop Up")]
        public Button[] popUpButtons;

        [Header("Player Inventory")]
        public Button playerInventoryExitButton;
        public Button eatButton;
        public Button discardButton;

        [Header("Exchange Inventory")]
        public Button exchangePurchaseButton;
        public Button exchangeInventoryExitButton;

        public CharacterMove characterMove;

        private GameObject[] uiScreen;
        private TurnController turnController;
        private PlayerInventoryUIController playerInventory;
        public bool isPlayerInventoryOpen { get; private set; } = false;
        public bool isExchangeInventoryOpen { get; private set; } = false;
        public bool isPopUpOpen { get; private set; } = false;
        public bool isTurnCheckOpen { get; private set; } = false;
        public bool isEventScriptOpen { get; private set; } = false;

        private void Awake()
        {
            turnController = GetComponentInParent<TurnController>();
            playerInventory = GetComponentInChildren<PlayerInventoryUIController>(true);
        }

        private void Start()
        {
            uiScreen = new GameObject[] { statusScreen, eventScriptScreen, turnCheckScreen, playerInventoryScreen, exchangeInventoryScreen, popUpScreen };
            for (int i = 0; i < uiScreen.Length; i++)
            {
                uiScreen[i].SetActive(false);
            }
            playerInventoryExitButton.onClick.RemoveAllListeners();
            playerInventoryExitButton.onClick.AddListener(ClosePlayerInventory);
        }

        private void Update()
        {
            ControlStatusScreen();
            ControlTurnCheckScreen();
            ControlPlayerInventoryScreen();
            InactiveEventScriptScreen();
        }

        #region Status Screen
        private void ControlStatusScreen()
        {
            if (SimpleInput.GetKeyDown(Key.Tab))
                ScreenOn(statusScreen);
            if (SimpleInput.GetKeyUp(Key.Tab))
                ScreenOff(statusScreen);
        }
        #endregion

        #region Event Script Screen

        public void ControlEventScript()
        {
            ActiveEventScriptScreen();
            InactiveEventScriptScreen();
        }

        public void ActiveEventScriptScreen()
        {
            isEventScriptOpen = true;
            characterMove.canMove = false;
            ScreenOn(eventScriptScreen);

        } // 이후 Text 수정 필요

        public void InactiveEventScriptScreen()
        {
            if (!isEventScriptOpen)
                return;
            if (SimpleInput.GetKeyDown(Key.Space) || SimpleInput.GetKeyDown(Key.Enter))
            {
                GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.uiMove);
                isEventScriptOpen = false;
                ScreenOff(eventScriptScreen);
                if (!isPopUpOpen && !isPlayerInventoryOpen)
                    characterMove.canMove = true;
            }
                
        }

        #endregion

        #region Turn Check Screen

        private void ControlTurnCheckScreen()
        {
            if (turnController.isTimeFlow && isTurnCheckOpen == false)
            {
                isTurnCheckOpen = true;
                ScreenOn(turnCheckScreen);
                StartCoroutine(TurnCheckScreenOffCoroutine());
            }
        }

        #endregion

        #region Player Inventory Screen

        private void ControlPlayerInventoryScreen()
        {
            if (isPopUpOpen)
                return;
            if (isPlayerInventoryOpen == false)
            {
                if (SimpleInput.GetKeyDown(Key.I))
                {
                    playerInventory.SetObjectsInactive();
                    playerInventory.isBeastInventory = false;
                    GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.openInvenUI);
                    ScreenOn(playerInventoryScreen);
                    isPlayerInventoryOpen = true;
                }
            }
            else
            {
                if (SimpleInput.GetKeyDown(Key.I))
                {
                    ClosePlayerInventory();
                }
                if (SimpleInput.GetKeyDown(Key.Escape))
                {
                    ClosePlayerInventory();
                }
            }
        }

        public void ClosePlayerInventory()
        {
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
            ScreenOff(playerInventoryScreen);
            playerInventory.isBeastInventory = false;
            isPlayerInventoryOpen = false;

            characterMove.canMove = true;
        }

        public void ActivePlayerInventoryInForce()
        {
            if (isPopUpOpen)
                return;
            playerInventory.SetObjectsInactive();
            OpenBeastInventory();
        }

        private void OpenBeastInventory()
        {
            isPlayerInventoryOpen = true;
            characterMove.canMove = false;
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.openInvenUI);
            ScreenOn(playerInventoryScreen);
        }

        #endregion

        #region Exchange Inventory Screen

        public void ControlExchangeInventoryScreen()
        {
            if (isPopUpOpen)
                return;

            isExchangeInventoryOpen = true;
            characterMove.canMove = false;

            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.openInvenUI);
            ScreenOn(exchangeInventoryScreen);
            exchangeInventoryExitButton.onClick.AddListener(CloseExchangeInventoryByButton);
            exchangePurchaseButton.onClick.AddListener(ControlPopUpScreen);
        }

        public void CloseExchangeInventoryScreenByKeyboard()
        {
            if (SimpleInput.GetKeyDown(Key.Escape))
            {
                ScreenOff(exchangeInventoryScreen);
                isExchangeInventoryOpen = false;
                characterMove.canMove = true;
            }
        }

        private void CloseExchangeInventoryByButton()
        {
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);

            ScreenOff(exchangeInventoryScreen);
            isExchangeInventoryOpen = false;
            characterMove.canMove = true;
        }

        #endregion

        #region Pop-up Screen

        public void OpenPopUp()
        {
            if (isEventScriptOpen)
            {
                ClosePopUp();
                return;
            }
            isPopUpOpen = true;
            characterMove.canMove = false;
            ScreenOn(popUpScreen);
        }

        public void ControlPopUpScreen()
        {
            OpenPopUp();
            for (int i = 0; i < popUpButtons.Length; ++i)
            {
                popUpButtons[i].onClick.AddListener(ClosePopUp);
            }    
                
        }

        public void ClosePopUp()
        {
            isPopUpOpen = false;
            ScreenOff(popUpScreen);
            if (!isEventScriptOpen && !isPlayerInventoryOpen)
                characterMove.canMove = true;
        }


        #endregion


        public void ScreenOn(GameObject uiScreen)
        {
            uiScreen.SetActive(true);
        }

        public void ScreenOff(GameObject uiScreen)
        {
            uiScreen.SetActive(false);
        }

        private IEnumerator TurnCheckScreenOffCoroutine()
        {
            yield return new WaitForSeconds(3.0f);
            ScreenOff(turnCheckScreen);
            turnController.isTimeFlow = false;
            isTurnCheckOpen = false;

            if (!isPopUpOpen && !isEventScriptOpen && !isPlayerInventoryOpen)
                characterMove.canMove = true;
        }
    }
}