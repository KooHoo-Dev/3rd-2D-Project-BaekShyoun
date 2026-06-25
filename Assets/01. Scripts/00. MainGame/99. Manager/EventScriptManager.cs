using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ObservedProject
{
    public class EventScriptManager : MonoBehaviour
    {
        public TextMeshProUGUI eventScriptText;

        public void Start()
        {
            SampleText();
        }

        public void SampleText()
        {
            eventScriptText.text = "내가 사라져 볼게";
        }

        public void EatMeat()
        {
            eventScriptText.text = $"";
        }

        public void DrinkWater()
        {
            eventScriptText.text = $"";
        }
    }

}