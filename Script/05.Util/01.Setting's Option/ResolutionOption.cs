using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionOption : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    private List<string> options; // 클래스 변수로 이동

    private void Start()
    {
        InitUI();

        resolutionDropdown.value = 1; // Default Index is 1.
        resolutionDropdown.RefreshShownValue();
        Screen.SetResolution(1920, 1080, Screen.fullScreenMode); // Default Resolution is FHD.
    }
    
    private void InitUI()
    {
        resolutionDropdown.options.Clear();

        // Resolution List
        options = new List<string>
        {
            "2560x1440",
            "1920x1080",    // default Index
            "1366x768"
        };

        foreach (string option in options)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(option));
        }

        resolutionDropdown.onValueChanged.AddListener(delegate { ChangeResolution(resolutionDropdown.value); });
    }

    public void ChangeResolution(int resolutionNum)
    {
        FullScreenMode currentScreenMode = Screen.fullScreenMode;

        switch (resolutionNum)
        {
            case 0:
                Screen.SetResolution(2560, 1440, currentScreenMode);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, currentScreenMode);
                break;
            case 2:
                Screen.SetResolution(1366, 768, currentScreenMode);
                break;
        }

        Debug.Log("Resolution changed to: " + options[resolutionNum]);
    }
}
