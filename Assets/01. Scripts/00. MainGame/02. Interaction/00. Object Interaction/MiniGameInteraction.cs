using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ObservedProject
{
    public class MiniGameInteraction : ObjectInteraction
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
                    uiInteraction.ControlPopUpScreen();
            }
        }
    }
}