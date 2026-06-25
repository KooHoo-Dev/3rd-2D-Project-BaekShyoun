using UnityEngine;
using TMPro;

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
    }

}