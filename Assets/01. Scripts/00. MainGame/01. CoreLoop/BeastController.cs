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
    public int maxNumber { get; private set; } = 101;
    public int minNumber { get; private set; } = 0;

    #region ForBeast

    public void FeedFreshMeat()
    {
        GageUpdate(escapeDesire, -feedDecreaseEscape);
    }

    public void FeedOtherMeat()
    {
        FeedFreshMeat();
        GageUpdate(attackPercentage, feedIncreaseAttack);
    }

    public void ChangedTurn(int skipTime)
    {
        GageUpdate(escapeDesire, turnIncreaseEscape * skipTime);
    }

    #endregion

    public void GageUpdate(int currentGage, int gage)
    {
        currentGage += gage;
        if (currentGage >= maxNumber) currentGage = maxNumber;
        if (currentGage <= minNumber) currentGage = minNumber;
    }
}
