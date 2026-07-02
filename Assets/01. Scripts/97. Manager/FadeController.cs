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

            color = fadeImage.color;
            color.a = 0.0f;
            fadeImage.color = color;

            while (true)
            {
                if (color.a >= maxAlpha) break;
                color.a += 0.05f;
                fadeImage.color = color;
                yield return new WaitForSeconds(0.02f);
            }

            color.a = maxAlpha;
            fadeImage.color = color;
        }

        public IEnumerator FadeOutCoroutine()
        {
            fadeImage.enabled = true;

            color = fadeImage.color;
            color.a = 1.0f;
            fadeImage.color = color;

            while (true)
            {
                if (color.a <= minAlpha) break;
                color.a -= 0.05f;
                fadeImage.color = color;
                yield return new WaitForSeconds(0.02f);
            }

            color.a = minAlpha;
            fadeImage.color = color;
            fadeImage.enabled = false;
        }

        public IEnumerator FadeInAndOutCoroutine(float waitTime)
        {
            yield return StartCoroutine(FadeInCoroutine());

            if (waitTime > 0f)
                yield return new WaitForSeconds(waitTime);

            yield return StartCoroutine(FadeOutCoroutine());
        }
    }
}