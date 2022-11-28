using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer background;
    [SerializeField] float backScrollSpeed;

    void Start()
    {
        background.material.mainTextureOffset = Vector2.zero;       // 시작시 메인 텍스처의 offset 값을 초기화한다.
    }
    void Update()
    {
        Vector2 offset = background.material.mainTextureOffset;     // 백그라운드 메인 텍스처의 offset 값 대입.
        offset.y += backScrollSpeed * Time.deltaTime;               // offset의 y값을 스피드 만큼 증가.
        background.material.mainTextureOffset = offset;             // 백그라운드 메인 텍스처의 offset 값 변경.
    }
}
