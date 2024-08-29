using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextDestructionEffect : MonoBehaviour
{
    public Text textComponent; // 텍스트 게임 오브젝트의 Text 컴포넌트
    public float destructionForce = 10f; // 텍스트 문자가 날아가는 힘
    public float destructionTime = 2f; // 텍스트 문자가 사라지는 시간

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private List<Rigidbody> characterRigidbodies;

    void Start()
    {
        // 텍스트 메시 정보 가져오기
        //mesh = textComponent.mesh;
        vertices = mesh.vertices;
        triangles = mesh.triangles;

        // 텍스트 문자별 Rigidbody 생성
        characterRigidbodies = new List<Rigidbody>();
        for (int i = 0; i < vertices.Length; i += 3)
        {
            GameObject characterGO = new GameObject("Character");
            characterGO.transform.SetParent(transform);
            characterGO.transform.localPosition = vertices[triangles[i]];
            characterGO.AddComponent<MeshFilter>().mesh = mesh;
            characterGO.AddComponent<MeshRenderer>().material = textComponent.material;
            Rigidbody rb = characterGO.AddComponent<Rigidbody>();
            rb.AddForce(Random.insideUnitSphere * destructionForce, ForceMode.Impulse);
            characterRigidbodies.Add(rb);
        }

        // 원본 텍스트 비활성화
        textComponent.gameObject.SetActive(false);
    }

    void Update()
    {
        // 텍스트 문자가 바닥에 닿으면 사라지게 함
        for (int i = 0; i < characterRigidbodies.Count; i++)
        {
            if (characterRigidbodies[i].position.y < 0)
            {
                Destroy(characterRigidbodies[i].gameObject, destructionTime);
            }
        }
    }
}
