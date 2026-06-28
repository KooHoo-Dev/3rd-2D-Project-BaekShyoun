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
        private float currentValue = 100f;
        private float speed = 50.0f;

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

        public IEnumerator ThreeSecCoroutine()
        {
            countDown.gameObject.SetActive(true);
            countDown.text = "3";
            yield return new WaitForSeconds(1.0f);
            countDown.text = "2";
            yield return new WaitForSeconds(1.0f);
            countDown.text = "1";
            yield return new WaitForSeconds(1.0f);
            countDown.text = "Start!";
            yield return new WaitForSeconds(1.0f);
            isDecreased = true;
            countDown.gameObject.SetActive(false);
        }
    }
}