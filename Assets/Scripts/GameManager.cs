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
    }
    void Update()
    {
        Vector2 offset = background.material.mainTextureOffset;     // ��׶��� ���� �ؽ�ó�� offset �� ����.
        offset.y += backScrollSpeed * Time.deltaTime;               // offset�� y���� ���ǵ� ��ŭ ����.
        background.material.mainTextureOffset = offset;             // ��׶��� ���� �ؽ�ó�� offset �� ����.
    }
}
