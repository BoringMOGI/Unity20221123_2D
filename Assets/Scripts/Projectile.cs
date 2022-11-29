using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*
     * 1.일정 거리 멀어지면 삭제.
     *  1-1) float Vector3.Distance(Vector3, Vector3) : 두 위치의 거리를 float로 반환한다.
     *  
     * 2.일정 시간이 흐르면 삭제.
     *  2-1) Invoke : 함수 지연 호출 (*)
     *  2-2) Destroy : 삭제 함수에 지연 시간을 준다.
     *  2-3) 변수에 Time.time을 더해가면서 원하는 타이밍 체크. (*)
     *  2-4) Time.time : 게임 시작부터 현재까지 흐른 시간을 이용한 방법
     *  
     * 3.Rigidbody의 타입
     *  3-1) Dynamic : 힘과 중력을 받는다.
     *  3-2) Kinematic : 힘과 중력을 받지 않는다.
     *  3-3) Static : 질량이 무한이며 절대 움직이지 않는다.
     */        

    float timer;                // 초를 세는 변수.
    float destroyTime;          // 삭제되는 시간.
    Vector3 createPosition;     // 생성된 위치 (world)
    float moveSpeed;            // 이동 속도.
    int power;

    // 반환형이 없고 Projectile을 매개변수로 받는 함수.
    public event Action<Projectile> onReturnStorage;     // 저장소로 돌아가는 이벤트 델리게이트.

    // 일정 시간 후에 사라진다.    
    private void Start()
    {
        // Invoke : 함수 지연 호출
        // Invoke(nameof(DestroyProjectile), 2.0f);    // N초 뒤에 "DestoryProejctile"이라는 이름의 함수를 호출하라.
        // Destroy(gameObject, 2.0f);                  // N초 뒤에 gameobject를 삭제한다.
        // destroyTime = Time.time + 2.0f;

        // 거리에 따라서 삭제하고 싶다.
        createPosition = transform.position;            // 현재 나의 위치를 저장한다.
    }
    private void Update()
    {
        // 월드 좌표 : 변하는 않는 고정적인 위치.
        // 로컬 좌표 : 나를 기준으로 변하는 상대적 위치.
        // Time.deltaTime : 프레임 타임.

        transform.position += transform.up * moveSpeed * Time.deltaTime;   // direction 방향으로 초당 moveSpeed미터 움직인다.

        // Time.time은 게임이 시작되고 지금까지 흐른 시간을 담고있다.
        /*
        if (Time.time >= destroyTime)
            DestroyProjectile();

        // 매 프레임마다 timer에 Time.deltaTime을 더하는 행위는 현재까지 흐른 시간을 더하는 것과 동일하다.
        timer += Time.deltaTime;
        if (timer >= 2.0f)          // timer값이 2초가 흘렀다고 해서 "정확히" 2.0이 되지는 않는다. (ex:2.000001)
            DestroyProjectile();

        // 생성 위치에서 현재 위치까지의 거리를 구한다.
        // 이 거리가 N이상일 경우 오브젝트를 삭제한다.
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(createPosition, currentPosition);
        if (distance >= 5f)
            DestroyProjectile();  
        */
    }

    // Cannon에서 탄환을 만든 후 Setup을 통해서 초기 값을 전달한다.
    public void Setup(int power, float moveSpeed)
    {
        this.power = power;
        this.moveSpeed = moveSpeed;     // 매개변수 moveSpeed를 멤버변수 moveSpeed에 대입한다.
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
