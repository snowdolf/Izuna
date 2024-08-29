using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class StartMapFadeOut : MonoBehaviour
{
    public ScreenFader screenFader;
    public float fadeOutDuration = 1.0f;

    private void Start()
    {
        if (screenFader != null)
        {
            StartCoroutine(screenFader.FadeIn(fadeOutDuration));
        }
        else
        {
            Debug.LogError("ScreenFader Error.");
        }
    }
}
