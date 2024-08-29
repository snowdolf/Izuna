using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatingSim : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // UI 텍스트 컴포넌트
    public GameObject dialogueBox;  // 대화 상자 UI
    public TextMeshProUGUI dialogNpcName;
    private string[] dialogueLines;  // 대화 문장 배열
    private int currentLineIndex = 0;  // 현재 대화 인덱스

    void Start()
    {

        // 대화 데이터 초기화 (실제 게임에서는 파일이나 데이터베이스에서 가져올 수 있음)
        dialogueLines = new string[]
        {
            Managers.Data.dialogues[1].Content,
            Managers.Data.dialogues[2].Content,
            Managers.Data.dialogues[3].Content,
            Managers.Data.dialogues[4].Content,
            Managers.Data.dialogues[5].Content,
            Managers.Data.dialogues[6].Content,
            Managers.Data.dialogues[7].Content
        };

        // 시작 시 대화 상자 비활성화
        dialogueBox.SetActive(false);

        // 시작 시 대화 시작
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1f);  // 시작 대기시간 (필요에 따라 조절)

        // 대화 상자 활성화
        dialogueBox.SetActive(true);
        dialogNpcName.text = Managers.Data.dialogues[1].NPCID;

        Time.timeScale = 0.0f;
        // 대화 시작
        while (currentLineIndex < dialogueLines.Length)
        {
            Debug.Log(dialogueLines[currentLineIndex]);
            dialogueText.text = dialogueLines[currentLineIndex];

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));  // 마우스 클릭 대기

            currentLineIndex++;  // 다음 대화로 넘어감

            // 모든 대화가 끝났을 때 대화 상자 비활성화
            if (currentLineIndex >= dialogueLines.Length)
            {
                dialogueBox.SetActive(false);
            }

            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = 1.0f;
    }
}