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

        // 무기 활성화.
        weapon.Using(muzzle);
        weapon.Fire();
    }

    public void Setup(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;     // 속도 값 대입.
        weapon.Init();                  // 무기 초기화.
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
