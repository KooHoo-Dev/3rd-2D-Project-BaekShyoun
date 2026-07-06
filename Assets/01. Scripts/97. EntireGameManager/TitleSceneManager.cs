using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ObservedProject
{
    public class TitleSceneManager : MonoBehaviour
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
            GameSoundManager.Instance.PlayBGM(GameSoundManager.Instance.titleBGM);

            GameStartButton.onClick.AddListener(StartGame);
            GameEndButton.onClick.AddListener(EndGame);
        }

        private void StartGame()
        {
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.startButton);
            StartCoroutine(StartGameCoroutine());
        }

        private IEnumerator StartGameCoroutine()
        {
            yield return StartCoroutine(fadeController.FadeInCoroutine());
            SceneManager.LoadScene(1);
        }

        private void EndGame()
        {
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.buttonClick);
            Application.Quit();
        }
    }
}