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
        public GameObject inventoryScreen;
        public Button inventoryExitButton;

        private GameObject[] uiScreen;
        private TurnController turnController;
        private bool isInventoryOpen = false;

        private void Awake()
        {
            inventoryExitButton.onClick.AddListener(CloseInventoryWithButton);
            turnController = GetComponentInParent<TurnController>();
        }

        private void Start()
        {
            uiScreen = new GameObject[] { statusScreen, eventScriptScreen, turnCheckScreen, inventoryScreen };
            for (int i = 0; i < uiScreen.Length; i++)
            {
                uiScreen[i].SetActive(false);
            }
        }

        private void Update()
        {
            ControllStatusScreen();
            ControllEventScriptScreen();
            ControllTurnCheckScreen();
            ControlInventoryScreen();
        }

        private void ControllStatusScreen()
        {
            if (SimpleInput.GetKeyDown(Key.Tab))
                ScreenOn(statusScreen);
            if (SimpleInput.GetKeyUp(Key.Tab))
                ScreenOff(statusScreen);
        }

        private void ControllEventScriptScreen() // 이후 수정 필요
        {
            if(SimpleInput.GetKeyDown(Key.V))
                ScreenOn(eventScriptScreen);
            if (SimpleInput.GetKeyDown(Key.Space) || SimpleInput.GetKeyDown(Key.Enter))
                ScreenOff(eventScriptScreen);
        }

        private void ControllTurnCheckScreen()
        {
            if (turnController.isTimeFlow)
            {
                ScreenOn(turnCheckScreen);
                StartCoroutine(TurnCheckScreenOffCoroutine());
            }
        }

        private void ControlInventoryScreen()
        {
            if (isInventoryOpen==false)
            {
                if (SimpleInput.GetKeyDown(Key.I))
                {
                    ScreenOn(inventoryScreen);
                    isInventoryOpen = true;
                }
            }
            else
            {
                if (SimpleInput.GetKeyDown(Key.I))
                {
                    ScreenOff(inventoryScreen);
                    isInventoryOpen = false;
                }
            }
            if (SimpleInput.GetKeyDown(Key.Escape))
                ScreenOff(inventoryScreen);
        }

        private void ScreenOn(GameObject uiScreen)
        {
            uiScreen.SetActive(true);
        }

        private void ScreenOff(GameObject uiScreen)
        {
            uiScreen.SetActive(false);
        }

        private void CloseInventoryWithButton()
        {
            inventoryScreen.SetActive(false);
        }

        private IEnumerator TurnCheckScreenOffCoroutine()
        {
            yield return new WaitForSeconds(3.0f);
            ScreenOff(turnCheckScreen);
            turnController.isTimeFlow = false;
        }
    }
}