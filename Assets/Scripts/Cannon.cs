using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    enum WEAPON
    {
        Single, // 한 줄기
        Double, // 두 줄기
        Radial, // 방사 형태
    }

    [SerializeField] float moveSpeed;               // 나의 이동 소곧.
    [SerializeField] Transform weaponPivot;         // 무기가 있어야할 위치(=기준점)
    [SerializeField] Weapon weapon;                 // 컴포넌트를 통해 전달하는 무기.
    Weapon currentWeapon;                           // 실제 내가 들고 있는 무기.

    private void Update()
    {
        // 캐릭터를 위 아래로 움직일 수 있다.
        float y = Input.GetAxisRaw("Vertical");
        transform.position += Vector3.up * moveSpeed * y * Time.deltaTime;

        if (weapon != null)                     // weapon이 null이 아닐 경우.
        {            
            if (currentWeapon != weapon)        // 현재 무기와 weapon이 다를 경우.
            {
                currentWeapon = weapon;         // 현재 무기를 weapon으로 대입.
                currentWeapon.Init();           // 현재 무기를 초기화.
            }

            currentWeapon.Using(weaponPivot);   // 현재 무기에게 사용중임을 알림.
            if (Input.GetKey(KeyCode.Z))        // 특정 키를 입력하면
                currentWeapon.Fire();           // Fire함수 호출.
        }
        else                                    // weapon이 null이라면.
        {
            currentWeapon = null;               // 현재 무기를 null로 대입.
        }
    }
}
