using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;

    public GameObject scanObject;
    public bool isAction;

    public int talkIndex;

    public void Action(GameObject scanObj)
    {
        if (isAction)
        {
            isAction = false;
        }
        else
        {
            isAction = true;
            scanObject = scanObj;
            NPC npc = scanObject.GetComponent<NPC>();
            Talk(npc.id);

        }
        talkPanel.SetActive(isAction);
    }

    void Talk(int id)
    {
        string talkData = dialogueManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;

        }

        isAction = true;
        talkIndex++;
    }
}