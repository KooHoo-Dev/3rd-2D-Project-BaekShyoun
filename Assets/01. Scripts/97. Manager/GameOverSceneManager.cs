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

    private void Start()
    {
        StartCoroutine(GoBackToTitle());
    }

    private void Update()
    {
        SetTextAboutDeath();
    }

    private void SetTextAboutDeath()
    {
        if (GameManagerMain.Instance.isDeadByDisease)
            reasonOfDeath.text = "당신은 질병으로 인해 사망했습니다!";
        if (GameManagerMain.Instance.isDeadByBeast)
            reasonOfDeath.text = "당신은 탈출한 짐승에게 잡아먹혔습니다!";
    }

    private IEnumerator GoBackToTitle()
    {
        yield return new WaitUntil(() => SimpleInput.GetKeyDown(Key.Enter));
        SceneManager.LoadScene(0);
    }
}
