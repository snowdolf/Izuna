using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour
{
    [SerializeField] private Canvas selection;
    [SerializeField] private Canvas OptionScreen;
    [SerializeField] private Canvas ExitScreen;
    [SerializeField] private Animator settingToggleAnimator;
    [SerializeField] private Image fadeImage;


    private void Start()
    {
        settingToggleAnimator = GetComponentInChildren<Animator>();
        fadeImage = GetComponentInChildren<Image>();
    }

    public void NewGame()
    {
        StartCoroutine(EffectManager.Instance.FadeIn(fadeImage, 1.0f, "Map1"));
    }

    public void ActivateOptionScreen()
    {
        selection.gameObject.SetActive(!selection.gameObject.activeSelf);
        settingToggleAnimator.SetBool("isOpen", true);
    }
    public void CloseOptionScreen()
    {
        if (OptionScreen.gameObject.activeSelf == true)
        {
            selection.gameObject.SetActive(true);
            settingToggleAnimator.SetBool("isOpen", false);
        }
    }

    public void ActivateExitScreen()
    {
        selection.gameObject.SetActive(!selection.gameObject.activeSelf);
        ExitScreen.gameObject.SetActive(!ExitScreen.gameObject.activeSelf);
    }

    public void GameExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OptionScreen.gameObject.activeSelf)
            {
                selection.gameObject.SetActive(true);
                settingToggleAnimator.SetBool("isOpen", false);
            }

            if (ExitScreen.gameObject.activeSelf)
            {
                selection.gameObject.SetActive(true);
                ExitScreen.gameObject.SetActive(false);
            }
        }
    }
}
