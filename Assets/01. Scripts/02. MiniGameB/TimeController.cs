using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ObservedProject
{
    public class TimeController : MonoBehaviour
    {
        [field: SerializeField] public Slider timeBar { get; private set; }
        [field: SerializeField] public TextMeshProUGUI countDown { get; private set; }
        public bool isDecreased { get; private set; } = false;
        public float currentValue = 100f;
        private float speed = 80.0f;

        private void Awake()
        {
            countDown.gameObject.SetActive(false);
        }

        private void Update()
        {
            UpdateTimeBar();
        }

        public void UpdateTimeBar()
        {
            if (isDecreased == false) return;
            currentValue -= (speed * Time.deltaTime);
            currentValue = Mathf.Clamp(currentValue, timeBar.minValue, timeBar.maxValue);
            timeBar.value = currentValue;
        }

        public void ResetTimeBar()
        {
            currentValue = 100.0f;
            timeBar.value = currentValue;
            isDecreased = false;
        }

        public IEnumerator ThreeSecCoroutine()
        {
            countDown.gameObject.SetActive(true);
            countDown.text = "3";
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.countDownB);
            yield return new WaitForSeconds(1.0f);
            countDown.text = "2";
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.countDownB);
            yield return new WaitForSeconds(1.0f);
            countDown.text = "1";
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.countDownB);
            yield return new WaitForSeconds(1.0f);
            countDown.text = "Start!";
            GameSoundManager.Instance.PlaySFX(GameSoundManager.Instance.gameStart);
            yield return new WaitForSeconds(1.0f);
            isDecreased = true;
            countDown.gameObject.SetActive(false);
        }
    }
}