using System.Collections;
using TMPro;
using UnityEngine;

namespace ObservedProject
{
    public class TimeFlow : MonoBehaviour
    {
        public TextMeshProUGUI countDownText;
        public HorseMove[] horseMoves;

        private void Awake()
        {
            countDownText.gameObject.SetActive(false);
        }

        public IEnumerator CountDownCoroutine()
        {
            countDownText.gameObject.SetActive(true);
            countDownText.text = "3";
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.countDown);
            yield return new WaitForSeconds(1.0f);
            countDownText.text = "2";
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.countDown);
            yield return new WaitForSeconds(1.0f);
            countDownText.text = "1";
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.countDown);
            yield return new WaitForSeconds(1.0f);
            countDownText.text = "Go!";
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.raceStart);
            yield return new WaitForSeconds(0.2f);

            GameSoundManager.Instance.PlayLoop(GameSoundManager.Instance.horseRun);

            for (int i = 0; i < horseMoves.Length; ++i)
            {
                horseMoves[i].StartRace();
            }
        }
    }
}
