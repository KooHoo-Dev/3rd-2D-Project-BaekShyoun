using ObservedProject;
using Study.Utilities;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour
{
    public TextMeshProUGUI reasonOfDeath;
    private FadeController fadeController;

    private void Awake()
    {
        fadeController = GetComponentInChildren<FadeController>();
    }

    private void Start()
    {
        GameSoundManager.Instance.PlayBGM(GameSoundManager.Instance.gameOverBGM);

        GameManagerMain.Instance.fadeController.fadeImage.enabled = false;
        StartCoroutine(fadeController.FadeOutCoroutine());
        StartCoroutine(GoBackToTitle());
    }

    private void Update()
    {
        SetTextAboutDeath();
    }

    private void SetTextAboutDeath()
    {
        if (GameManagerMain.Instance.isDeadByDisease)
            reasonOfDeath.text = "당신은 질병으로 인해\n사망했습니다!";
        if (GameManagerMain.Instance.isDeadByBeast)
            reasonOfDeath.text = "당신은 탈출한 짐승에게\n잡아먹혔습니다!";
    }

    private IEnumerator GoBackToTitle()
    {
        yield return new WaitUntil(() => SimpleInput.GetKeyDown(Key.Enter));

        GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.returnToTitle);

        yield return new WaitForSeconds(0.3f);

        GameManagerMain.Instance.DestroyManager();
        SceneManager.LoadScene(0);
    }
}
