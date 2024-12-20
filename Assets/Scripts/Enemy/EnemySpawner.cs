using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일정 시간마다 Enemy를 현재 위치에 소환합니다.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("Variable")]
    [SerializeField] private float spawnTimer = 5f;

    [Header("Essential")]
    [SerializeField] private GameObject Enemy;
    [SerializeField] private MyObjectPool EnemyPool;
    
    private WaitForSeconds wfs;

    private void Awake()
    {
        wfs = new WaitForSeconds(spawnTimer);
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return wfs;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        GameObject enemy = EnemyPool.GetFromPool();
        enemy.transform.position = transform.position;
    }
}
