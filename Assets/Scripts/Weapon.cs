using UnityEngine;

public enum WEAPON
{
    Single, // 한 줄기
    Double, // 두 줄기
    Radial, // 방사 형태
}

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WEAPON type;
    [SerializeField] protected float fireRate;      // 연사 속도.
    [SerializeField] protected float bulletSpeed;   // 총알 속도.
    [SerializeField] protected int power;           // 총알 공격력.
    [SerializeField] protected Transform[] pivots;  // 중심점 배열.

    public WEAPON Type => type;
    protected float fireDelay;     // 딜레이 타임.

    // 추상 메서드.
    // 1.실제 구현부가 없다.
    // 2.추상 클래스 내부에서만 사용할 수 있다.
    // 3.추상 클래스는 독립적으로 존재할 수 없다.
    // 4.추상 클래스를 상속하면 추상클래스를 "무조건" 구현해야한다.
    public virtual void Init() 
    {
        fireDelay = fireRate;       // 딜레이 타임 초기화.
    }                        
    public virtual void Using(Transform weaponPivot)
    {
        // 무기 기준점을 따라한다.
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
