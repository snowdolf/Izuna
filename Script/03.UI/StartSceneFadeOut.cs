using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneFadeOut : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    void Start()
    {
        fadeImage = GetComponent<Image>();
        StartCoroutine(Fade(fadeImage, 2.0f, 2.0f));
    }

    IEnumerator Fade(Image image, float fadeOutDuration, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        yield return StartCoroutine(EffectManager.Instance.FadeOut(image, fadeOutDuration));
    }
}
