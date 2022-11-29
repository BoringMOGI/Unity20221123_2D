using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*
     * 1.���� �Ÿ� �־����� ����.
     *  1-1) float Vector3.Distance(Vector3, Vector3) : �� ��ġ�� �Ÿ��� float�� ��ȯ�Ѵ�.
     *  
     * 2.���� �ð��� �帣�� ����.
     *  2-1) Invoke : �Լ� ���� ȣ�� (*)
     *  2-2) Destroy : ���� �Լ��� ���� �ð��� �ش�.
     *  2-3) ������ Time.time�� ���ذ��鼭 ���ϴ� Ÿ�̹� üũ. (*)
     *  2-4) Time.time : ���� ���ۺ��� ������� �帥 �ð��� �̿��� ���
     *  
     * 3.Rigidbody�� Ÿ��
     *  3-1) Dynamic : ���� �߷��� �޴´�.
     *  3-2) Kinematic : ���� �߷��� ���� �ʴ´�.
     *  3-3) Static : ������ �����̸� ���� �������� �ʴ´�.
     */        

    float timer;                // �ʸ� ���� ����.
    float destroyTime;          // �����Ǵ� �ð�.
    Vector3 createPosition;     // ������ ��ġ (world)
    float moveSpeed;            // �̵� �ӵ�.
    int power;

    // ��ȯ���� ���� Projectile�� �Ű������� �޴� �Լ�.
    public event Action<Projectile> onReturnStorage;     // ����ҷ� ���ư��� �̺�Ʈ ��������Ʈ.

    // ���� �ð� �Ŀ� �������.    
    private void Start()
    {
        // Invoke : �Լ� ���� ȣ��
        // Invoke(nameof(DestroyProjectile), 2.0f);    // N�� �ڿ� "DestoryProejctile"�̶�� �̸��� �Լ��� ȣ���϶�.
        // Destroy(gameObject, 2.0f);                  // N�� �ڿ� gameobject�� �����Ѵ�.
        // destroyTime = Time.time + 2.0f;

        // �Ÿ��� ���� �����ϰ� �ʹ�.
        createPosition = transform.position;            // ���� ���� ��ġ�� �����Ѵ�.
    }
    private void Update()
    {
        // ���� ��ǥ : ���ϴ� �ʴ� �������� ��ġ.
        // ���� ��ǥ : ���� �������� ���ϴ� ����� ��ġ.
        // Time.deltaTime : ������ Ÿ��.

        transform.position += transform.up * moveSpeed * Time.deltaTime;   // direction �������� �ʴ� moveSpeed���� �����δ�.

        // Time.time�� ������ ���۵ǰ� ���ݱ��� �帥 �ð��� ����ִ�.
        /*
        if (Time.time >= destroyTime)
            DestroyProjectile();

        // �� �����Ӹ��� timer�� Time.deltaTime�� ���ϴ� ������ ������� �帥 �ð��� ���ϴ� �Ͱ� �����ϴ�.
        timer += Time.deltaTime;
        if (timer >= 2.0f)          // timer���� 2�ʰ� �귶�ٰ� �ؼ� "��Ȯ��" 2.0�� ������ �ʴ´�. (ex:2.000001)
            DestroyProjectile();

        // ���� ��ġ���� ���� ��ġ������ �Ÿ��� ���Ѵ�.
        // �� �Ÿ��� N�̻��� ��� ������Ʈ�� �����Ѵ�.
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(createPosition, currentPosition);
        if (distance >= 5f)
            DestroyProjectile();  
        */
    }

    // Cannon���� źȯ�� ���� �� Setup�� ���ؼ� �ʱ� ���� �����Ѵ�.
    public void Setup(int power, float moveSpeed)
    {
        this.power = power;
        this.moveSpeed = moveSpeed;     // �Ű����� moveSpeed�� ������� moveSpeed�� �����Ѵ�.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if(tag == "Destroy")
        {
            onReturnStorage?.Invoke(this);
        }
        if(tag == "Enemy")
        {
            // .......
            Enemy target = collision.GetComponent<Enemy>();
            target.OnDamaged(power);
            onReturnStorage?.Invoke(this);
        }
    }
}
