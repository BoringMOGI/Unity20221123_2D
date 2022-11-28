using UnityEngine;

public enum WEAPON
{
    Single, // �� �ٱ�
    Double, // �� �ٱ�
    Radial, // ��� ����
}

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WEAPON type;
    [SerializeField] protected float fireRate;      // ���� �ӵ�.
    [SerializeField] protected float bulletSpeed;   // �Ѿ� �ӵ�.
    [SerializeField] protected int power;           // �Ѿ� ���ݷ�.
    [SerializeField] protected Transform[] pivots;  // �߽��� �迭.

    public WEAPON Type => type;
    protected float fireDelay;     // ������ Ÿ��.

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
                bullet.Setup(power, bulletSpeed);
            }
        }
    }
}
