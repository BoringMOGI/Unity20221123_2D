using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer background;
    [SerializeField] float backScrollSpeed;

    void Start()
    {
        background.material.mainTextureOffset = Vector2.zero;       // ���۽� ���� �ؽ�ó�� offset ���� �ʱ�ȭ�Ѵ�.

        // Random.Range(float, float)   min�̻� max�̸��� ���̰��� �ش�.
        // Random.value                 0.0 ~ 1.0 ���̰��� �ش�.

        // Prefab variant
        // => ���� �����տ��� �Ϻκ��� �ٸ��� �����ϰ� ���� �� ����Ѵ�.
        //    ���� �������� ����Ǹ� ���� �����հ� ������ �κ��� ���� ����ȴ�.
        //    ������ �������� ���� �����տ� �ݿ����� �ʴ´�.

        // �̺�Ʈ �Լ��� �ڷ�ƾ���� ����ϱ�
        // => Start�� ����Ƽ�� �ڵ����� �ҷ��ִ� �̺�Ʈ �Լ���.
        //    �̰��� �ڷ�ƾ���� �����ϸ� �ڵ����� �ڷ�ƾ�� ȣ���ϰ� �ȴ�.

        // Material
        // => �׷����� ǥ���ϴ� ����(����)
    
        // Shader
        // => Texture(�̹���)�� ��� �׸� ���ΰ��� ǥ���ϴ� ���.

    }
    void Update()
    {
        Vector2 offset = background.material.mainTextureOffset;     // ��׶��� ���� �ؽ�ó�� offset �� ����.
        offset.y += backScrollSpeed * Time.deltaTime;               // offset�� y���� ���ǵ� ��ŭ ����.
        background.material.mainTextureOffset = offset;             // ��׶��� ���� �ؽ�ó�� offset �� ����.
    }
}
