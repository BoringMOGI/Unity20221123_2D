using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStorage : MonoBehaviour
{
    [SerializeField] Projectile poolPrefab;     // 풀링 오브젝트 프리팹.
    [SerializeField] Transform storageParent;   // 저장소 부모 오브젝트.
    [SerializeField] int initCount;             // 최초 생성 개수.

    public static BulletStorage Instance { get; private set; }

    Stack<Projectile> storage = new Stack<Projectile>();    // 인스턴스된 클론들이 저장될 공간.

    // Instantiate : 생성
    // Destroy : 삭제
    // 오브젝트 풀링 : 최초에 미리 많이 생성해두고 필요할 때마다가 꺼내 쓴다. 다 쓰면 반납하는 기법.
    //               많은 오브젝트가 다량으로 생성될 때 성능 저하를 막기위해 고안되었다.
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // 저장된 풀링이 꺼져있어야 하기 때문에 부모 오브젝트인 storageParent를 끈다.
        storageParent.gameObject.SetActive(false);
        for(int i = 0; i< initCount; i++)
            CreatePool();
    }

    private void CreatePool()
    {
        Projectile pool = Instantiate(poolPrefab, Vector3.zero, Quaternion.identity, storageParent);
        pool.onReturnStorage += ReturnPool;     // 이벤트 등록.
        storage.Push(pool);
    }

    public Projectile GetPool()
    {
        // 혹시라도 저장된 풀링이 다 소모되었을 경우.
        if (storage.Count <= 0)
            CreatePool();

        // 예외 처리를 했기 때문에 이 시점에서 Pop이 실패할 일은 없다.
        Projectile pool = storage.Pop();
        pool.transform.SetParent(transform);    // 부모 오브젝트를 storageParent에서 나로 변경한다.
        return pool;
    }
    private void ReturnPool(Projectile pool)
    {
        // 되돌아온 풀링 오브젝트를 저장소에 넣는다.
        pool.transform.SetParent(storageParent);
        storage.Push(pool);
    }
}