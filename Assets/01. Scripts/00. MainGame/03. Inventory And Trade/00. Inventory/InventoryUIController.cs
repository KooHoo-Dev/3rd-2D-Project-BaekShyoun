using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ObservedProject
{
    public class InventoryUIController : MonoBehaviour
    {
        public Button[] itemButtons;
        public Button eatButton;
        public Button discardButton;
        public TextMeshProUGUI description;

        private void Awake()
        {
            SetObjectsInactive();
            description.text = string.Empty;
        }

        private void Update()
        {
            for (int i = 0; i < itemButtons.Length; ++i)
            {
                itemButtons[i].onClick.AddListener(SetObjectsActive);
            }
        }

        private void SetObjectsActive()
        {
            eatButton.gameObject.SetActive(true);
            discardButton.gameObject.SetActive(true);
            description.gameObject.SetActive(true);
            SetDescription();
        }

        private void SetObjectsInactive()
        {
            eatButton.gameObject.SetActive(false);
            discardButton.gameObject.SetActive(false);
            description.gameObject.SetActive(false);
        }

        private void SetDescription()
        {
            description.text = "로또 맞고 싶다";
        }
    }
}