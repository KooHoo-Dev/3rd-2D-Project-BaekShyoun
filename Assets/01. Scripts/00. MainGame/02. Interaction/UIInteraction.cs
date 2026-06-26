using Study.Utilities;
using System.Collections;
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

        private GameObject[] uiScreen;
        private TurnController turnController;
        private bool isInventoryOpen = false;

        private void Awake()
        {
            turnController = GetComponentInParent<TurnController>();
        }

        private void Start()
        {
            uiScreen = new GameObject[] { statusScreen, eventScriptScreen, turnCheckScreen, playerInventoryScreen, exchangeInventoryScreen, popUpScreen };
            for (int i = 0; i < uiScreen.Length; i++)
            {
                uiScreen[i].SetActive(false);
            }
        }

        private void Update()
        {
            ControlStatusScreen();
            ControlTurnCheckScreen();
            ControlPlayerInventoryScreen();
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

        public void ActiveEventScriptScreen()
        {
            ScreenOn(eventScriptScreen);

        } // 이후 Text 수정 필요

        public void InactiveEventScriptScreen()
        {
            if (SimpleInput.GetKeyDown(Key.Space) || SimpleInput.GetKeyDown(Key.Enter))
                ScreenOff(eventScriptScreen);
        }

        #endregion

        #region Turn Check Screen

        private void ControlTurnCheckScreen()
        {
            if (turnController.isTimeFlow)
            {
                ScreenOn(turnCheckScreen);
                StartCoroutine(TurnCheckScreenOffCoroutine());
            }
        }

        #endregion

        #region Player Inventory Screen

        private void ControlPlayerInventoryScreen()
        {
            if (isInventoryOpen == false)
            {
                if (SimpleInput.GetKeyDown(Key.I))
                {
                    ScreenOn(playerInventoryScreen);
                    isInventoryOpen = true;
                    eatButton.onClick.AddListener(ControlPopUpScreen);
                    discardButton.onClick.AddListener(ControlPopUpScreen);
                }
            }
            else
            {
                if (SimpleInput.GetKeyDown(Key.I))
                {
                    InactivePlayerInventory();
                    isInventoryOpen = false;
                }
                if (SimpleInput.GetKeyDown(Key.Escape))
                    InactivePlayerInventory();
                playerInventoryExitButton.onClick.AddListener(InactivePlayerInventory);
            }
        } // x버튼이나 Esc로 닫았을 때 I를 누르면 입력이 한 번 씹힘 > 일단 다른 기능 추가 후 수정

        private void InactivePlayerInventory()
        {
            ScreenOff(playerInventoryScreen);
        }

        #endregion

        #region Exchange Inventory Screen

        public void ControlExchangeInventoryScreen()
        {
            ScreenOn(exchangeInventoryScreen);
            exchangeInventoryExitButton.onClick.AddListener(CloseExchangeInventoryByButton);
            exchangePurchaseButton.onClick.AddListener(ControlPopUpScreen);
        }  // 구매 후 창이 닫히면 좋을 것 같은데, 여러개 계속 살 거면 그냥 열려 있는 게 나을 거 같기도... 추후 수정

        public void CloseExchangeInventoryScreenByKeyboard()
        {
            if (SimpleInput.GetKeyDown(Key.Escape))
                ScreenOff(exchangeInventoryScreen);
        }

        private void CloseExchangeInventoryByButton()
        {
            ScreenOff(exchangeInventoryScreen);
        }

        #endregion

        #region Pop-up Screen

        public void ControlPopUpScreen()
        {
            ScreenOn(popUpScreen);
            for (int i = 0; i < popUpButtons.Length; ++i)
                popUpButtons[i].onClick.AddListener(ClosePopUpByButton);
        } // 이후 Text 수정 필요

        private void ClosePopUpByButton()
        {
            ScreenOff(popUpScreen);
        }
        #endregion


        private void ScreenOn(GameObject uiScreen)
        {
            uiScreen.SetActive(true);
        }

        private void ScreenOff(GameObject uiScreen)
        {
            uiScreen.SetActive(false);
        }

        private IEnumerator TurnCheckScreenOffCoroutine()
        {
            yield return new WaitForSeconds(3.0f);
            ScreenOff(turnCheckScreen);
            turnController.isTimeFlow = false;
        }
    }
}