using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Texts : MonoBehaviour
{
    public TextMeshProUGUI texts;
    private string abcdefg = "Fxxk";
    private int enlargedFontSize = 48; // 원하는 폰트 크기

    void Start()
    {
        // GetComponent로 텍스트 컴포넌트를 가져오기 전에 null 확인
        if (texts == null)
        {
            texts = GetComponent<TextMeshProUGUI>();
        }

        if (texts != null)
        {
            transFontSize(100, 0, "Hello", texts);
        }
        else
        {
            Debug.LogError("TextMeshProUGUI 컴포넌트를 찾을 수 없습니다.");
        }
    }

    void Update()
    {
    }



    void transFontSize(int size, int sequence, string textContent, TextMeshProUGUI txt)
    {
        string sequenceChar = textContent[sequence].ToString();
        string restOfString = textContent.Substring(sequence + 1);
        txt.text = $"<size={size}>{sequenceChar}</size>{restOfString}";
    }
}
