using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EffectManager : Singleton<EffectManager>
{
    public IEnumerator FadeOutAndIn(Image image, float fadeOutDuration, float waitingTime, float fadeInDuration)
    {
        yield return new WaitForSeconds(waitingTime);

        yield return StartCoroutine(FadeOut(image, fadeOutDuration));

        yield return new WaitForSeconds(waitingTime);

        yield return StartCoroutine(FadeIn(image, fadeInDuration));

        SceneManager.LoadScene("StartScene");
    }
    public IEnumerator FadeOut(Image image, float duration)
    {
        float startAlpha = image.color.a;
        float endAlpha = 0.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            SetAlpha(image, newAlpha);
            yield return null;
        }
        SetAlpha(image, endAlpha);
    }

    public IEnumerator FadeIn(Image image, float duration)
    {
        float startAlpha = image.color.a;
        float endAlpha = 1.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            SetAlpha(image, newAlpha);
            yield return null;
        }
        SetAlpha(image, endAlpha);
    }
    public IEnumerator FadeIn(Image image, float duration, string scene)
    {
        float startAlpha = image.color.a;
        float endAlpha = 1.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            SetAlpha(image, newAlpha);
            yield return null;
        }
        SetAlpha(image, endAlpha);

        SceneManager.LoadScene(scene);
    }

    private void SetAlpha(Image image, float alpha)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}
