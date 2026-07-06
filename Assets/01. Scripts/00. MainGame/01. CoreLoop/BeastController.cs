using ObservedProject;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BeastController : MonoBehaviour
{
    public enum BeastState
    {
        Calm,
        Alert,
        Agitated,
        Danger
    }

    // Beast Gage Controller
    [Header("BeastSettings")]
    public int escapeDesire = 0;
    public int attackPercentage = 50;
    public int turnIncreaseEscape = 20;
    public int feedDecreaseEscape = 10;
    public int feedIncreaseAttack = 5;
    public int maxNumber { get; private set; } = 100;
    public int minNumber { get; private set; } = 0;

    public UIInteraction uiInteraction;
    public TextMeshProUGUI eventScriptText;
    public GameObject spawnedBeast;

    public bool hasBeast = true;

    private void Start()
    {
        eventScriptText.text = string.Empty;
        spawnedBeast.SetActive(true);
    }
    private void Update()
    {
        EscapeBeast();
    }

    public void FeedFreshMeat()
    {
        SetGage();
        escapeDesire = GageUpdate(escapeDesire, -feedDecreaseEscape);
    }

    public void FeedOtherMeat()
    {
        SetGage();
        FeedFreshMeat();
        attackPercentage = GageUpdate(attackPercentage, feedIncreaseAttack);
    }

    public void ChangedTurn(int skipTime)
    {
        SetGage();
        escapeDesire = GageUpdate(escapeDesire, turnIncreaseEscape * skipTime);
        if (escapeDesire > 50)
        {
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.beastGrowl);
        }
    }


    public int GageUpdate(int currentGage, int gage)
    {
        currentGage += gage;
        if (currentGage >= maxNumber) currentGage = maxNumber;
        if (currentGage <= minNumber) currentGage = minNumber;
        return currentGage;
    }

    public void ShowBeastStateText(BeastState state, TextMeshProUGUI eventScriptText)
    {
        Debug.Log("ShowBeastStateText 실행됨");
        switch(state)
        {
            case BeastState.Calm:
                eventScriptText.text = $"맹수는 편안하게 휴식을 취하고 있다.\n탈출욕구 : {escapeDesire}";
                break;
            case BeastState.Alert:
                eventScriptText.text = $"맹수는 불안한지 우리 안을 서성이고 있다.\n탈출욕구 : {escapeDesire}";
                break;
            case BeastState.Agitated:
                if (attackPercentage < 50)
                    eventScriptText.text = $"굶주린 맹수가 울부짖고 있다.\n탈출욕구 : {escapeDesire}";
                else
                    eventScriptText.text = $"굶주린 맹수가 당신을 응시하고 있다.\n탈출욕구 : {escapeDesire}";
                break;
            case BeastState.Danger:
                if (attackPercentage < 50)
                    eventScriptText.text = $"맹수는 당장이라도 탈출할 것처럼 철창을 긁고 있다.\n탈출욕구 : {escapeDesire}";
                else
                    eventScriptText.text = $"굶주린 맹수가 당신을 노려보며 울부짖고 있다.\n탈출욕구 : {escapeDesire}";
                break;
        }
    }
    public void CheckBeastStatusMachine(TextMeshProUGUI eventScriptText)
    {
        Debug.Log("CheckBeastStatusMachine 실행됨");
        if (0 <= escapeDesire && escapeDesire <= 25)
            ShowBeastStateText(BeastState.Calm, eventScriptText);
        if (25 < escapeDesire && escapeDesire <= 50)
            ShowBeastStateText(BeastState.Alert, eventScriptText);
        if (50 < escapeDesire && escapeDesire <= 75)
            ShowBeastStateText(BeastState.Agitated, eventScriptText);
        if (75 < escapeDesire && escapeDesire <= 100)
            ShowBeastStateText(BeastState.Danger, eventScriptText);
    }

    public void EscapeBeast()
    {
        if (hasBeast == false)
            return;
        if (escapeDesire >= maxNumber)
        {
            hasBeast = false;
            spawnedBeast.SetActive(false);
            escapeDesire = maxNumber;
            uiInteraction.ControlEventScript();
            eventScriptText.text = "배고픔을 견디지 못한 맹수가 탈출했습니다!";
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.beastRoar);


            GameManagerMain.Instance.GameOverByBeast();
        }
    }

    public void SpawnBeast()
    {
        if (hasBeast == true)
            return;
        hasBeast = true;
        escapeDesire = minNumber;
        spawnedBeast.SetActive(true);
    }

    public void SetGage()
    {
        if (escapeDesire < minNumber) escapeDesire = minNumber;
        if (escapeDesire > maxNumber) escapeDesire = maxNumber;
        if (attackPercentage < minNumber) attackPercentage = minNumber;
        if (attackPercentage > maxNumber) attackPercentage = maxNumber;
    }
}
