using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStorage : MonoBehaviour
{
    [SerializeField] Projectile poolPrefab;     // Ǯ�� ������Ʈ ������.
    [SerializeField] Transform storageParent;   // ����� �θ� ������Ʈ.
    [SerializeField] int initCount;             // ���� ���� ����.

    public static BulletStorage Instance { get; private set; }

    Stack<Projectile> storage = new Stack<Projectile>();    // �ν��Ͻ��� Ŭ�е��� ����� ����.

    // Instantiate : ����
    // Destroy : ����
    // ������Ʈ Ǯ�� : ���ʿ� �̸� ���� �����صΰ� �ʿ��� �����ٰ� ���� ����. �� ���� �ݳ��ϴ� ���.
    //               ���� ������Ʈ�� �ٷ����� ������ �� ���� ���ϸ� �������� ��ȵǾ���.
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // ����� Ǯ���� �����־�� �ϱ� ������ �θ� ������Ʈ�� storageParent�� ����.
        storageParent.gameObject.SetActive(false);
        for(int i = 0; i< initCount; i++)
            CreatePool();
    }

    private void CreatePool()
    {
        Projectile pool = Instantiate(poolPrefab, Vector3.zero, Quaternion.identity, storageParent);
        pool.onReturnStorage += ReturnPool;     // �̺�Ʈ ���.
        storage.Push(pool);
    }

    public Projectile GetPool()
    {
        // Ȥ�ö� ����� Ǯ���� �� �Ҹ�Ǿ��� ���.
        if (storage.Count <= 0)
            CreatePool();

        // ���� ó���� �߱� ������ �� �������� Pop�� ������ ���� ����.
        Projectile pool = storage.Pop();
        pool.transform.SetParent(transform);    // �θ� ������Ʈ�� storageParent���� ���� �����Ѵ�.
        return pool;
    }
    private void ReturnPool(Projectile pool)
    {
        // �ǵ��ƿ� Ǯ�� ������Ʈ�� ����ҿ� �ִ´�.
        pool.transform.SetParent(storageParent);
        storage.Push(pool);
    }
}