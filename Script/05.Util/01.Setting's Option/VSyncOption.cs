using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSyncOption : MonoBehaviour
{
    public Dropdown vsyncDropdown;

    private void Start()
    {
        QualitySettings.vSyncCount = 0; // Default VSync Off

        InitUI();
        vsyncDropdown.value = 0; // Default VSync Off
        vsyncDropdown.RefreshShownValue();
    }

    private void InitUI()
    {
        vsyncDropdown.options.Clear();

        // VSync List
        List<string> options = new List<string>
        {
            "끄기",
            "켜기"
        };

        foreach (string option in options)
        {
            vsyncDropdown.options.Add(new Dropdown.OptionData(option));
        }

        vsyncDropdown.onValueChanged.AddListener(delegate { ChangeVSync(vsyncDropdown.value); });
    }

    public void ChangeVSync(int vsyncNum)
    {
        switch (vsyncNum)
        {
            case 0:
                QualitySettings.vSyncCount = 0;
                break;
            case 1:
                QualitySettings.vSyncCount = 1;
                break;
        }

        Debug.Log("VSync setting changed to: " + (vsyncNum == 0 ? "Off" : "On"));
    }
}
