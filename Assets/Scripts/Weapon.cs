using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireDelay;     // ������ Ÿ��.
    [SerializeField] protected float fireRate;      // ���� �ӵ�.
    [SerializeField] protected float bulletSpeed;   // �Ѿ� �ӵ�.
    [SerializeField] protected Transform[] pivots;  // �߽��� �迭.

    // �߻� �޼���.
    // 1.���� �����ΰ� ����.
    // 2.�߻� Ŭ���� ���ο����� ����� �� �ִ�.
    // 3.�߻� Ŭ������ ���������� ������ �� ����.
    // 4.�߻� Ŭ������ ����ϸ� �߻�Ŭ������ "������" �����ؾ��Ѵ�.
    public virtual void Init() 
    {
        fireDelay = fireRate;       // ������ Ÿ�� �ʱ�ȭ.
    }                        
    public virtual void Using(Transform weaponPivot)
    {
        // ���� �������� �����Ѵ�.
        transform.position = weaponPivot.position;
        transform.rotation = weaponPivot.rotation;
        fireDelay -= Time.deltaTime;
    }
    public virtual void Fire()
    {
        if (fireDelay <= 0)
        {
            fireDelay = fireRate;
            foreach (Transform pivot in pivots)
            {
                Projectile bullet = BulletStorage.Instance.GetPool();
                bullet.transform.position = pivot.transform.position;
                bullet.transform.rotation = pivot.transform.rotation;
                bullet.Setup(bulletSpeed);
            }
        }
    }
}
