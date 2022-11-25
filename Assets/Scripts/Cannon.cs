using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform muzzle;              // �ѱ�.
    [SerializeField] float fireRate;                // ���� �ӵ�.
    [SerializeField] float fireSpeed;               // �ӵ�.
    [SerializeField] float moveSpeed;               // ���� �̵� �Ұ�.

    float fireTime = 0.0f;

    private void Update()
    {
        // ĳ���͸� �� �Ʒ��� ������ �� �ִ�.
        float y = Input.GetAxisRaw("Vertical");
        transform.position += Vector3.up * moveSpeed * y * Time.deltaTime;

        // �����̽��ٸ� ������ �� �߻��Ѵ�.
        // || (OR������)  : ���� �߿� �ϳ��� ���̸� ���̴�.
        // && (AND������) : ���� ���� ���̸� ���̴�.
        fireTime -= Time.deltaTime;

        // �����̽� Ű�� ������ �����鼭 fireTime�� ���� 0.0f���� ���� ���.
        if(Input.GetKey(KeyCode.Space) && fireTime <= 0.0f)
        {
            fireTime = fireRate;

            Projectile projectile = BulletStorage.Instance.GetPool();   // ������Ʈ Ǯ���� ������ ����ҿ��� �ϳ��� �����´�.
            projectile.transform.position = muzzle.position;            // muzzle�� ��ġ�� ����.
            projectile.transform.rotation= muzzle.rotation;             // muzzle�� ȸ���� ����.
            projectile.Setup(fireSpeed);
        }
    }

}
