using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform muzzle;              // 총구.
    [SerializeField] float fireRate;                // 연사 속도.
    [SerializeField] float fireSpeed;               // 속도.
    [SerializeField] float moveSpeed;               // 나의 이동 소곧.

    float fireTime = 0.0f;

    private void Update()
    {
        // 캐릭터를 위 아래로 움직일 수 있다.
        float y = Input.GetAxisRaw("Vertical");
        transform.position += Vector3.up * moveSpeed * y * Time.deltaTime;

        // 스페이스바를 눌렀을 때 발사한다.
        // || (OR연산자)  : 양쪽 중에 하나라도 참이면 참이다.
        // && (AND연산자) : 양쪽 전부 참이면 참이다.
        fireTime -= Time.deltaTime;

        // 스페이스 키를 누르고 있으면서 fireTime의 값이 0.0f보다 작을 경우.
        if(Input.GetKey(KeyCode.Space) && fireTime <= 0.0f)
        {
            fireTime = fireRate;

            Projectile projectile = BulletStorage.Instance.GetPool();   // 오브젝트 풀링을 구현한 저장소에서 하나를 빌려온다.
            projectile.transform.position = muzzle.position;            // muzzle의 위치값 복사.
            projectile.transform.rotation= muzzle.rotation;             // muzzle의 회전값 복사.
            projectile.Setup(fireSpeed);
        }
    }

}
