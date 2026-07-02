using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ObservedProject
{
    public class TitleManager : MonoBehaviour
    {
        [field: SerializeField] public Button GameStartButton { get; private set; }
        [field: SerializeField] public Button GameEndButton { get; private set; }
        private FadeController fadeController;

        private void Awake()
        {
            fadeController = GetComponentInChildren<FadeController>();
        }
        private void Start()
        {
            GameStartButton.onClick.AddListener(StartGame);
            GameEndButton.onClick.AddListener(EndGame);
        }

        private void StartGame()
        {
            StartCoroutine(fadeController.FadeInCoroutine());
            SceneManager.LoadScene(1);
        }

        private void EndGame()
        {
            Application.Quit();
        }
    }
}