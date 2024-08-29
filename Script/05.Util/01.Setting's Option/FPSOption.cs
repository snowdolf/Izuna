using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSOption : MonoBehaviour
{
    public Dropdown fpsDropdown;

    private void Start()
    {
        InitUI();
        fpsDropdown.value = 0; // Default Fps is 60.
        fpsDropdown.RefreshShownValue();

        Application.targetFrameRate = 60;
    }

    private void InitUI()
    {
        fpsDropdown.options.Clear();

        // Fps List
        List<string> options = new List<string>
        {
            "60 FPS",
            "80 FPS",
            "120 FPS",
            "144 FPS",
            "240 FPS"
        };

        foreach (string option in options)
        {
            fpsDropdown.options.Add(new Dropdown.OptionData(option));
        }

        fpsDropdown.onValueChanged.AddListener(delegate { ChangeFPS(fpsDropdown.value); });
    }

    public void ChangeFPS(int fpsIndex)
    {
        int targetFPS = 60;

        switch (fpsIndex)
        {
            case 0:
                targetFPS = 60;
                break;
            case 1:
                targetFPS = 80;
                break;
            case 2:
                targetFPS = 120;
                break;
            case 3:
                targetFPS = 144;
                break;
            case 4:
                targetFPS = 240;
                break;
        }

        Application.targetFrameRate = targetFPS;
        Debug.Log("FPS setting changed to: " + targetFPS + " FPS");
    }
}
