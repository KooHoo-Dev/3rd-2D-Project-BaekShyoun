using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ObservedProject
{
    public class UIInteraction : MonoBehaviour
    {
        // 1. Status창 : 기본 비활성화 > Tab키 입력 시 활성화
        // 2. EventScript창 : 기본 비활성화 > EventScript 발생시 활성화 (Enter키 or Space키 입력 후 비활성화)
        // 3. TurnCheck창 : 기본 비활성화 > 턴 변화시/일 변화시 잠시 활성화 후 Alpha값 서서히 감소
        // 4. Inventory창 : 기본 비활성화 > I키 입력시 활성화 (Esc키 or X버튼 클릭 후 비활성화) > Inventory창 내부 상호작용은 따로 짤 거임

        [Header("UI Screen")]
        public GameObject StatusScreen;
        public GameObject EventScriptScreen;
        public GameObject TurnCheckScreen;
        public GameObject InventoryScreen;
        public Button InventoryExitButton;

        private GameObject[] uiScreen;
        private TurnController turnController;

        private void Awake()
        {
            InventoryExitButton.onClick.AddListener(CloseInventory);
            turnController = GetComponentInParent<TurnController>();
        }

        private void Start()
        {
            uiScreen = new GameObject[] { StatusScreen, EventScriptScreen, TurnCheckScreen, InventoryScreen };
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
                ScreenOn(StatusScreen);
            if (SimpleInput.GetKeyUp(Key.Tab))
                ScreenOff(StatusScreen);
        }

        private void ControllEventScriptScreen()
        {

        }

        private void ControllTurnCheckScreen()
        {
            if (turnController.isTimeFlow)
            {
                ScreenOn(TurnCheckScreen);
            }
        }

        private void ControlInventoryScreen()
        {
            if (SimpleInput.GetKeyDown(Key.I))
                ScreenOn(InventoryScreen);
            if (SimpleInput.GetKeyDown(Key.Escape))
                ScreenOff(InventoryScreen);
        }

        private void ScreenOn(GameObject uiScreen)
        {
            uiScreen.SetActive(true);
        }
        private void ScreenOff(GameObject uiScreen)
        {
            uiScreen.SetActive(false);
        }
        private void CloseInventory()
        {
            InventoryScreen.SetActive(false);
        }
    }
}