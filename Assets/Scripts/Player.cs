using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;               // 나의 이동 소곧.
    [SerializeField] Transform weaponPivot;         // 무기가 있어야할 위치(=기준점)
    [SerializeField] BoxCollider2D limitArea;       // 제한 공간.

    Weapon[] weapons;                               // 사용가능한 무기들 (캐싱)
    Weapon currentWeapon;                           // 실제 내가 들고 있는 무기.
    Bounds limitBounds;                             // 화면 상 내부의 경계.

    private void Start()
    {
        // 캐싱(Cashing)
        // => 게임 도중에 검색을 하는 행위는 필요할 때 마다 도구를 창고에서 가져오는 행위와 같다. (오래 걸린다.)
        //    따라서 미리 시작할 때 다~ 불러와서 참조시키는 방법.
        weapons = Resources.LoadAll<Weapon>("Weapon");    // Resources\Load 디렉토리 내의 Weapon 파일을 전부 가져와라.
        limitBounds = limitArea.bounds;                   // 내부 경계 값을 최초에 대입한다. (단, 지금은 화면이 움직이지 않기 때문)
    }
    private void Update()
    {
        // 캐릭터를 위 아래로 움직일 수 있다.
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 dir = Vector3.up * y + Vector3.right * x;           // 이동 방향.
        Vector3 movement = dir * moveSpeed * Time.deltaTime;        // 이동량.
        Vector3 position = transform.position + movement;           // 내가 있어야할 위치.

        // ClosePoint(Vector3) : Vector3
        // => 매개변수 위치 값이 경계 내부에 있는지 확인하고
        //    경계 외부에 있다면 가장 가까운 경계 지점 위치 값을 반환한다.
        transform.position = limitBounds.ClosestPoint(position);    // 최종 나의 위치를 대입.

        if (currentWeapon == null)
            return;

        currentWeapon.Using(weaponPivot);
        if (Input.GetKey(KeyCode.Z))        // 특정 키를 입력하면
            currentWeapon.Fire();           // Fire함수 호출.
    }

    public void OnChangeWeapon(int index)
    {
        WEAPON type = (WEAPON)index;
        if (currentWeapon?.Type == type)     // currentWeapon이 null이 아닐때 Type이 type과 같다면
        {
            Debug.Log("같은 무기 변경은 불가능합니다.");
            return;
        }

        foreach (Weapon weapon in weapons)
        {
            if (weapon.Type == type)
            {
                Debug.Log($"무기 변경 : {currentWeapon}");

                currentWeapon = weapon;     // 현재 무기를 weapon으로 변경.
                currentWeapon.Init();       // 새로 바뀐 무기 초기화.
                return;
            }
        }
    }
}
