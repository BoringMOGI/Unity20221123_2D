using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform minPosition;     // 생성 최소 위치.
    [SerializeField] Transform maxPosition;     // 생성 최대 위치.
    [SerializeField] Enemy[] enemyPrefabs;         // 적 프리팹.
    [SerializeField] float spawnRate;           // 생성 주기.
    [SerializeField] int spawnCount;            // 생성 개수.

    // 이벤트 함수인 Start에 IEnumerator를 반환형으로 선언하면
    // 시작과 동시에 코루틴이 자동 호출된다.
    IEnumerator Start()
    {
        int remaining = spawnCount;     // 남은 스폰 개수.
        while(remaining > 0)            // 남은 개수가 0보다 클 때.
        {
            float dealy = Random.Range(spawnRate * 0.7f, spawnRate * 1.3f);     // spwanRate에 오차를 준다.
            yield return new WaitForSeconds(dealy);                             // delay초 만큼 기다린다.
            remaining -= 1;                                                     // 남은 개수를 1 줄인다.

            // 적 프리팹 배열에서 하나를 골라 생성.
            Enemy prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Enemy newEnemy = Instantiate(prefab, GetSpawnPosition(), Quaternion.identity, transform);
            newEnemy.Setup(Random.Range(1f, 3f));
        }
    }
    private Vector3 GetSpawnPosition()
    {
        Vector3 createPos = minPosition.position;
        Vector3 dir = (maxPosition.position - minPosition.position).normalized;
        float distance = Vector3.Distance(minPosition.position, maxPosition.position);
        createPos += dir * Random.Range(0, distance);

        return createPos;
    }
}
