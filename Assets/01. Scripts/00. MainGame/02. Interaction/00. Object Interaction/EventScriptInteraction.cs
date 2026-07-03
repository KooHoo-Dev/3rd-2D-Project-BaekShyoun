using Study.Utilities;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static BeastController;

namespace ObservedProject
{
    public class EventScriptInteraction : ObjectInteraction
    {
        public TextMeshProUGUI eventScriptText;
        public GameObject beast;
        private BeastController beastController;

        protected override void Awake()
        {
            base.Awake();
            beastController = beast.GetComponent<BeastController>();
        }

        private void Update()
        {
            CheckPlayerState(CharacterMove.State.Back);
        }

        protected override void CheckPlayerState(CharacterMove.State state)
        {
            if (characterMove.currentState == state && canInteract )
            {
                if(SimpleInput.GetKeyDown(Key.F))
                {
                    uiInteraction.ActiveEventScriptScreen();
                    CheckObjects(this.name);
                }
                uiInteraction.InactiveEventScriptScreen();
            }
            else
            {
                uiInteraction.InactiveEventScriptScreen();
            }
        }

        private void CheckObjects(string name)
        {
            switch(name)
            {
                case "BeastStatusMachine":
                    CheckBeastStatusMachine();
                    break;
                case "BeastDescription":
                    CheckBeastDescription();
                    break;
                case "Door":
                    CheckDoor();
                    break;
                case "Clock":
                    CheckClock();
                    break;
                case "GameC" or "GameD":
                    CheckGameMachine();
                    break;
            }
        }

        private void CheckBeastStatusMachine()
        {
            eventScriptText.text = string.Empty;
            beastController.CheckBeastStatusMachine(eventScriptText);
        }

        private void CheckBeastDescription()
        {
            eventScriptText.text = string.Empty;
            eventScriptText.text = "종이에는 이렇게 적혀있다.\n<b>오래 살고 싶다면 잊지 말고 고기를 줄 것</b>";
        }

        private void CheckDoor()
        {
            eventScriptText.text = string.Empty;
            eventScriptText.text = "아무리 열어보려고 시도해도 꿈쩍도 하지 않는다.";
        }

        private void CheckClock()
        {
            eventScriptText.text = string.Empty;
            eventScriptText.text = "시계는 있지만 고장난 듯하다.";
        }

        private void CheckGameMachine()
        {
            eventScriptText.text = string.Empty;
            eventScriptText.text = "고장난 건지 작동하지 않는다.";
        }
    }
}