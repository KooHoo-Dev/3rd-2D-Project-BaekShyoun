using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

namespace ObservedProject
{
    public abstract class ObjectInteraction : MonoBehaviour
    {
        protected bool canInteract = false;
        public GameObject player;
        public GameObject uiInteract;
        protected CharacterMove characterMove;
        protected UIInteraction uiInteraction;

        protected virtual void Awake()
        {
            characterMove = player.GetComponent<CharacterMove>();
            uiInteraction = uiInteract.GetComponent<UIInteraction>();
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log($"{collision.name} Enter to {this.name}");
                canInteract = true;
            }
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log($"{collision.name} Exit from {this.name}");
                canInteract = false;
            }
        }

        protected abstract void CheckPlayerState(CharacterMove.State state);

    }
}