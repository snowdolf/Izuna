using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowModeOption : MonoBehaviour
{
    FullScreenMode screenMode;
    public Dropdown windowModeDropdown;

    private void Start()
    {
        InitUI();

        windowModeDropdown.value = 0; // Default is FullScreen
        windowModeDropdown.RefreshShownValue();

        screenMode = FullScreenMode.FullScreenWindow;
        Screen.fullScreenMode = screenMode;
    }
    
    private void InitUI()
    {
        windowModeDropdown.options.Clear();

        // WindowMode List
        List<string> options = new List<string>
        {
            "전체화면",
            "창모드"
        };

        foreach (string option in options)
        {
            windowModeDropdown.options.Add(new Dropdown.OptionData(option));
        }

        windowModeDropdown.onValueChanged.AddListener(delegate { DropBoxOptionChange(windowModeDropdown.value); });
    }

    public void DropBoxOptionChange(int windowModeNum)
    {
        switch (windowModeNum)
        {
            case 0:
                screenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                screenMode = FullScreenMode.Windowed;
                break;
        }
        Screen.fullScreenMode = screenMode;
        Debug.Log("Screen mode set to: " + screenMode);
    }
}
