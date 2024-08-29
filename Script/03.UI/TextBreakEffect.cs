using System.Collections;
using UnityEngine;
using TMPro;

public class TextBreakEffect : MonoBehaviour
{
    public TMP_Text textMeshPro; // 텍스트를 위한 TextMeshPro 객체
    public float breakDelay = 0.1f; // 문자 사이의 깨짐 지연 시간
    public float breakForce = 5f; // 문자 깨짐 힘
    public int moveSteps = 3; // 이동 횟수

    private TMP_TextInfo textInfo; // Dialogues 데이터를 받아올거임
    private bool isBreaking = false;

    void Start()
    {
        textMeshPro.ForceMeshUpdate();
        textInfo = textMeshPro.textInfo;
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space) && !isBreaking)
        // {
        //     StartCoroutine(BreakText());
        // }
        // 구현되는지 실험용으로 썼음. 대화 구현되면 선택창에서 제한시간 안에 선택못할 때 사용하거나, npc가 적대하는 대사를 골랐을 경우에 실행되게 만들거임.
    }

    IEnumerator BreakText()
    {
        isBreaking = true;

        // 문자들의 초기 위치와 분리된 후 이동 방향 설정
        Vector3[][] initialVertices = new Vector3[textInfo.meshInfo.Length][];

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            initialVertices[i] = new Vector3[textInfo.meshInfo[i].vertices.Length];
            System.Array.Copy(textInfo.meshInfo[i].vertices, initialVertices[i], textInfo.meshInfo[i].vertices.Length);
        }

        // 문자 색상 변경
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            Color32[] newVertexColors = textInfo.meshInfo[materialIndex].colors32;
            Color32 red = new Color32(255, 0, 0, 255);

            newVertexColors[vertexIndex + 0] = red;
            newVertexColors[vertexIndex + 1] = red;
            newVertexColors[vertexIndex + 2] = red;
            newVertexColors[vertexIndex + 3] = red;

            textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }

        // 문자 깨짐 효과 적용
        for (int step = 0; step < moveSteps; step++)
        {
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible)
                    continue;

                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                Vector3 offset = new Vector3(Random.Range(-breakForce, breakForce), Random.Range(-breakForce, breakForce), 0);
                vertices[vertexIndex + 0] += offset;
                vertices[vertexIndex + 1] += offset;
                vertices[vertexIndex + 2] += offset;
                vertices[vertexIndex + 3] += offset;

                textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            }

            yield return new WaitForSeconds(breakDelay);
        }

        isBreaking = false;
    }
}
