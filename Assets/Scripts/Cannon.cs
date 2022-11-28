using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] float moveSpeed;               // 나의 이동 소곧.
    [SerializeField] Transform weaponPivot;         // 무기가 있어야할 위치(=기준점)

    Weapon[] weapons;                               // 사용가능한 무기들 (캐싱)
    Weapon currentWeapon;                           // 실제 내가 들고 있는 무기.

    private void Start()
    {
        // 캐싱(Cashing)
        // => 게임 도중에 검색을 하는 행위는 필요할 때 마다 도구를 창고에서 가져오는 행위와 같다. (오래 걸린다.)
        //    따라서 미리 시작할 때 다~ 불러와서 참조시키는 방법.
        weapons = Resources.LoadAll<Weapon>("Weapon");    // Resources\Load 디렉토리 내의 Weapon 파일을 전부 가져와라.
    }
    private void Update()
    {
        // 캐릭터를 위 아래로 움직일 수 있다.
        float y = Input.GetAxisRaw("Vertical");
        transform.position += Vector3.up * moveSpeed * y * Time.deltaTime;

        if (currentWeapon == null)
            return;

        currentWeapon.Using(weaponPivot);
        if (Input.GetKey(KeyCode.Z))        // 특정 키를 입력하면
            currentWeapon.Fire();           // Fire함수 호출.
    }

    public void OnChangeWeapon(int index)
    {
        WEAPON type = (WEAPON)index;
        if(currentWeapon?.Type == type)     // currentWeapon이 null이 아닐때 Type이 type과 같다면
        {
            Debug.Log("같은 무기 변경은 불가능합니다.");
            return;
        }

        foreach(Weapon weapon in weapons)
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
