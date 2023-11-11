using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField, Min(0)] Vector2 m_timeToSpawnRange;
    float m_currentTimeToSpawn;
    [SerializeField] GameObject m_zombiePrefab;

    private void Awake()
    {
        ResetTimer();
    }

    private void Update()
    {
        m_currentTimeToSpawn -= Time.deltaTime;
        if (m_currentTimeToSpawn <= 0)
        {
            SpawnEnemy();
            ResetTimer();
        }
    }

    void SpawnEnemy()
    {
        Instantiate(m_zombiePrefab, transform.position, Quaternion.identity);
    }

    void ResetTimer()
    {
        m_currentTimeToSpawn = Random.Range(m_timeToSpawnRange.x, m_timeToSpawnRange.y);
    }
}
