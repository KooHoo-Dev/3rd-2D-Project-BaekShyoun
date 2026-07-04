using TMPro;
using UnityEngine;

namespace ObservedProject
{
    public class EventScriptTextManager : MonoBehaviour
    {
        public TextMeshProUGUI eventScriptText;
        private UIInteraction uiInteraction;

        private void Awake()
        {
            uiInteraction = GetComponentInChildren<UIInteraction>();
        }

        public void SetTextHaveMeat(int fullness, int diseaseStack)
        {
            eventScriptText.text = string.Empty;
            uiInteraction.ControlEventScript();
            eventScriptText.text = $"포만감 +{fullness}, 질병 상태 {diseaseStack}";
        }
        public void SetTextHaveWater(int hidration, int diseaseStack)
        {
            eventScriptText.text = string.Empty;
            uiInteraction.ControlEventScript();
            eventScriptText.text = $"수분 +{hidration}, 질병 상태 {diseaseStack}";
        }

        public void SetTextPlayGame(int fullness, int hidration, int energy, int diseaseStack)
        {
            eventScriptText.text = string.Empty;
            uiInteraction.ControlEventScript();
            eventScriptText.text = $"게임 플레이로 시간이 흘렀습니다!" +
                $"\n포만감 {fullness}, 수분 {hidration}, 에너지 {energy}, 질병 상태 {diseaseStack}";
        }

        public void SetTextTakeDeepSleep(int fullness, int hidration, int energy, int diseaseStack)
        {
            eventScriptText.text = string.Empty;
            uiInteraction.ControlEventScript();
            eventScriptText.text = $"수면권을 1개 사용하여 당신은 깊은 수면에 빠졌습니다!" +
                $"\n포만감 {fullness}, 수분 {hidration}, 에너지 +{energy}, 질병 상태 {diseaseStack}";
        }

        public void SetTextTakeNormalSleep(int fullness, int hidration, int energy, int diseaseStack)
        {
            eventScriptText.text = string.Empty;
            uiInteraction.ControlEventScript();
            eventScriptText.text = $"당신은 조금 불편한 잠을 청했습니다!" +
                $"\n포만감 {fullness}, 수분 {hidration}, 에너지 +{energy}, 질병 상태 {diseaseStack}";
        }
    }
}