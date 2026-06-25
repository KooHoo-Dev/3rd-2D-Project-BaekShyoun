using UnityEngine;

namespace ObservedProject
{
    public abstract class ObjectInteraction : MonoBehaviour
    {
        protected bool canInteract = false;

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

        protected void CheckPlayerState(CharacterMove.State state)
        {

        }
    }
}