using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ObservedProject
{
    public class FadeController: MonoBehaviour
    {
        public Image fadeImage;

        private Color color;
        private float maxAlpha = 1.0f;
        private float minAlpha = 0.0f;

        private void Awake()
        {
            fadeImage = GetComponent<Image>();
            fadeImage.enabled = false;
        }

        public IEnumerator FadeInCoroutine()
        {
            fadeImage.enabled = true;
            color.a = 0.0f;
            while(true)
            {
                color = fadeImage.color;
                if (color.a >= maxAlpha) yield break;
                color.a += 0.01f;
                fadeImage.color = color;
                Debug.Log(fadeImage.color);
                yield return new WaitForSeconds(0.02f);
            }
        }

        public IEnumerator FadeOutCoroutine()
        {
            color.a = 1.0f;
            while (true)
            {
                color = fadeImage.color;
                if (color.a <= minAlpha) yield break;
                color.a -= 0.01f;
                fadeImage.color = color;
                Debug.Log(fadeImage.color);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}