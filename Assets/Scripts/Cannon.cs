using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    enum WEAPON
    {
        Single, // �� �ٱ�
        Double, // �� �ٱ�
        Radial, // ��� ����
    }

    [SerializeField] float moveSpeed;               // ���� �̵� �Ұ�.
    [SerializeField] Transform weaponPivot;         // ���Ⱑ �־���� ��ġ(=������)
    [SerializeField] Weapon weapon;                 // ������Ʈ�� ���� �����ϴ� ����.
    Weapon currentWeapon;                           // ���� ���� ��� �ִ� ����.

    private void Update()
    {
        // ĳ���͸� �� �Ʒ��� ������ �� �ִ�.
        float y = Input.GetAxisRaw("Vertical");
        transform.position += Vector3.up * moveSpeed * y * Time.deltaTime;

        if (weapon != null)                     // weapon�� null�� �ƴ� ���.
        {            
            if (currentWeapon != weapon)        // ���� ����� weapon�� �ٸ� ���.
            {
                currentWeapon = weapon;         // ���� ���⸦ weapon���� ����.
                currentWeapon.Init();           // ���� ���⸦ �ʱ�ȭ.
            }

            currentWeapon.Using(weaponPivot);   // ���� ���⿡�� ��������� �˸�.
            if (Input.GetKey(KeyCode.Z))        // Ư�� Ű�� �Է��ϸ�
                currentWeapon.Fire();           // Fire�Լ� ȣ��.
        }
        else                                    // weapon�� null�̶��.
        {
            currentWeapon = null;               // ���� ���⸦ null�� ����.
        }
    }
}
