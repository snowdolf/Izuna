using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    public IEnumerator FadeOut(float duration)
    {
        canvasGroup.alpha = 0; // Ensure the alpha starts at 0
        float startAlpha = 0; // Always start from 0
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, t / duration);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    public IEnumerator FadeIn(float duration)
    {
        canvasGroup.alpha = 1; // Ensure the alpha starts at 1
        float startAlpha = 1; // Always start from 1
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, t / duration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}
