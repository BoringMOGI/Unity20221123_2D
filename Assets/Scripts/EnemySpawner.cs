using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform minPosition;     // ���� �ּ� ��ġ.
    [SerializeField] Transform maxPosition;     // ���� �ִ� ��ġ.
    [SerializeField] Enemy[] enemyPrefabs;         // �� ������.
    [SerializeField] float spawnRate;           // ���� �ֱ�.
    [SerializeField] int spawnCount;            // ���� ����.

    // �̺�Ʈ �Լ��� Start�� IEnumerator�� ��ȯ������ �����ϸ�
    // ���۰� ���ÿ� �ڷ�ƾ�� �ڵ� ȣ��ȴ�.
    IEnumerator Start()
    {
        int remaining = spawnCount;     // ���� ���� ����.
        while(remaining > 0)            // ���� ������ 0���� Ŭ ��.
        {
            float dealy = Random.Range(spawnRate * 0.7f, spawnRate * 1.3f);     // spwanRate�� ������ �ش�.
            yield return new WaitForSeconds(dealy);                             // delay�� ��ŭ ��ٸ���.
            remaining -= 1;                                                     // ���� ������ 1 ���δ�.

            // �� ������ �迭���� �ϳ��� ��� ����.
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
