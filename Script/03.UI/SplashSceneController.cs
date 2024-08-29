using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashSceneController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }
    void Start()
    {
        
        fadeImage = GetComponent<Image>();
        StartCoroutine(EffectManager.Instance.FadeOutAndIn(fadeImage, 2.0f, 2.0f, 1.0f));
    }
}
