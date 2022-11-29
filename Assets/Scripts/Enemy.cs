using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] Transform muzzle;
    [SerializeField] Weapon weapon;

    float moveSpeed;

    private void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y <= -13f)
            Destroy(gameObject);

        // ���� Ȱ��ȭ.
        weapon.Using(muzzle);
        weapon.Fire();
    }

    public void Setup(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;     // �ӵ� �� ����.
        weapon.Init();                  // ���� �ʱ�ȭ.
    }

    public void OnDamaged(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
