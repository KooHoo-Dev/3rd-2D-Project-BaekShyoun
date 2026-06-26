using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ObservedProject
{
    public class BedInteraction : ObjectInteraction
    {
        private void Update()
        {
            CheckPlayerPos();
        }

        protected override void CheckPlayerState(CharacterMove.State state)
        {
            if (characterMove.currentState == state && canInteract)
            {
                if (SimpleInput.GetKeyDown(Key.F))
                    uiInteraction.ControlPopUpScreen();
            }
        }

        private void CheckPlayerPos()
        {
            if (this.name == "Bed(Under)")
                CheckPlayerState(CharacterMove.State.Back);
            if (this.name == "Bed(Side)")
                CheckPlayerState(CharacterMove.State.Right);
            if (this.name == "Bed(Upper)")
                CheckPlayerState(CharacterMove.State.Front);
        }
    }
}
