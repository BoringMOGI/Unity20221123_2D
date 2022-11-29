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

        // Random.Range(float, float)   min이상 max미만의 사이값을 준다.
        // Random.value                 0.0 ~ 1.0 사이값을 준다.

        // Prefab variant
        // => 기존 프리팹에서 일부분을 다르게 변형하고 싶을 때 사용한다.
        //    기존 프리팹이 변경되면 기존 프리팹과 동일한 부분은 전부 변경된다.
        //    나만의 변경점은 기존 프리팹에 반영되지 않는다.

        // 이벤트 함수를 코루틴으로 사용하기
        // => Start는 유니티가 자동으로 불러주는 이벤트 함수다.
        //    이것을 코루틴으로 구현하면 자동으로 코루틴을 호출하게 된다.

        // Material
        // => 그래픽을 표현하는 재질(성질)
    
        // Shader
        // => Texture(이미지)를 어떻게 그릴 것인가를 표현하는 방법.

    }
    void Update()
    {
        Vector2 offset = background.material.mainTextureOffset;     // 백그라운드 메인 텍스처의 offset 값 대입.
        offset.y += backScrollSpeed * Time.deltaTime;               // offset의 y값을 스피드 만큼 증가.
        background.material.mainTextureOffset = offset;             // 백그라운드 메인 텍스처의 offset 값 변경.
    }
}
