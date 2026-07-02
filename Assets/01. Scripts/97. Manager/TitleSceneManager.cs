using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ObservedProject
{
    public class TitleSceneManager : MonoBehaviour
    {
        [field: SerializeField] public Button GameStartButton { get; private set; }
        [field: SerializeField] public Button GameEndButton { get; private set; }

        private void Start()
        {
            GameStartButton.onClick.AddListener(StartGame);
            GameEndButton.onClick.AddListener(EndGame);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        private void EndGame()
        {
            Application.Quit();
        }
    }
}