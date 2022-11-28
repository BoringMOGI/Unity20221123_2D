using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;               // ���� �̵� �Ұ�.
    [SerializeField] Transform weaponPivot;         // ���Ⱑ �־���� ��ġ(=������)
    [SerializeField] BoxCollider2D limitArea;       // ���� ����.

    Weapon[] weapons;                               // ��밡���� ����� (ĳ��)
    Weapon currentWeapon;                           // ���� ���� ��� �ִ� ����.
    Bounds limitBounds;                             // ȭ�� �� ������ ���.

    private void Start()
    {
        // ĳ��(Cashing)
        // => ���� ���߿� �˻��� �ϴ� ������ �ʿ��� �� ���� ������ â���� �������� ������ ����. (���� �ɸ���.)
        //    ���� �̸� ������ �� ��~ �ҷ��ͼ� ������Ű�� ���.
        weapons = Resources.LoadAll<Weapon>("Weapon");    // Resources\Load ���丮 ���� Weapon ������ ���� �����Ͷ�.
        limitBounds = limitArea.bounds;                   // ���� ��� ���� ���ʿ� �����Ѵ�. (��, ������ ȭ���� �������� �ʱ� ����)
    }
    private void Update()
    {
        // ĳ���͸� �� �Ʒ��� ������ �� �ִ�.
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 dir = Vector3.up * y + Vector3.right * x;           // �̵� ����.
        Vector3 movement = dir * moveSpeed * Time.deltaTime;        // �̵���.
        Vector3 position = transform.position + movement;           // ���� �־���� ��ġ.

        // ClosePoint(Vector3) : Vector3
        // => �Ű����� ��ġ ���� ��� ���ο� �ִ��� Ȯ���ϰ�
        //    ��� �ܺο� �ִٸ� ���� ����� ��� ���� ��ġ ���� ��ȯ�Ѵ�.
        transform.position = limitBounds.ClosestPoint(position);    // ���� ���� ��ġ�� ����.

        if (currentWeapon == null)
            return;

        currentWeapon.Using(weaponPivot);
        if (Input.GetKey(KeyCode.Z))        // Ư�� Ű�� �Է��ϸ�
            currentWeapon.Fire();           // Fire�Լ� ȣ��.
    }

    public void OnChangeWeapon(int index)
    {
        WEAPON type = (WEAPON)index;
        if (currentWeapon?.Type == type)     // currentWeapon�� null�� �ƴҶ� Type�� type�� ���ٸ�
        {
            Debug.Log("���� ���� ������ �Ұ����մϴ�.");
            return;
        }

        foreach (Weapon weapon in weapons)
        {
            if (weapon.Type == type)
            {
                Debug.Log($"���� ���� : {currentWeapon}");

                currentWeapon = weapon;     // ���� ���⸦ weapon���� ����.
                currentWeapon.Init();       // ���� �ٲ� ���� �ʱ�ȭ.
                return;
            }
        }
    }
}
