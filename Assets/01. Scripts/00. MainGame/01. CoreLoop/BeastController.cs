using ObservedProject;
using UnityEngine;

public class BeastController : MonoBehaviour
{
    // Beast Gage Controller
    [Header("BeastSettings")]
    public int escapeDesire = 0;
    public int attackPercentage = 0;
    public int turnIncreaseEscape = 20;
    public int feedDecreaseEscape = 10;
    public int feedIncreaseAttack = 5;
    public int maxNumber { get; private set; } = 100;
    public int minNumber { get; private set; } = 0;

    public UIInteraction uiInteraction;

    public void FeedFreshMeat()
    {
        escapeDesire = GageUpdate(escapeDesire, -feedDecreaseEscape);
    }

    public void FeedOtherMeat()
    {
        FeedFreshMeat();
        attackPercentage = GageUpdate(attackPercentage, feedIncreaseAttack);
    }

    public void ChangedTurn(int skipTime)
    {
        escapeDesire = GageUpdate(escapeDesire, turnIncreaseEscape * skipTime);
    }


    public int GageUpdate(int currentGage, int gage)
    {
        currentGage += gage;
        if (currentGage >= maxNumber) currentGage = maxNumber;
        if (currentGage <= minNumber) currentGage = minNumber;
        return currentGage;
    }

    public void EscapeBeast()
    {
        if (escapeDesire >= maxNumber) uiInteraction.ControlEventScript();
    }
}
