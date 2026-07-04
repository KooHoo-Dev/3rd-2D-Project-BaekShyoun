using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ObservedProject
{
    public class ExchangeMachineInteraction : ObjectInteraction
    {
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
                    uiInteraction.ControlExchangeInventoryScreen();
                    characterMove.canMove = false;
                }
                uiInteraction.CloseExchangeInventoryScreenByKeyboard();
            }
        }
    }

}