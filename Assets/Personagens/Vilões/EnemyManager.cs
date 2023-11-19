using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] m_spawnPoints;
    public GameObject[] m_enemyPrefab;
    public int[] m_numberOfEnemiesToSpawn;

    public void Start()
    {
        SpawnNewEnemy();
    }
    
    public void OnEnable()
    {
        VidaAI.OnEnemyKilled += SpawnNewEnemy;
    }

    public void SpawnNewEnemy()
    {
        if (m_spawnPoints != null && m_spawnPoints.Length > 0 && m_enemyPrefab != null && m_enemyPrefab.Length > 0 && m_numberOfEnemiesToSpawn != null && m_numberOfEnemiesToSpawn.Length > 0)
        {
            int randomEnemyIndex = Random.Range(0, m_enemyPrefab.Length);
            
            for (int i = 0; i < m_numberOfEnemiesToSpawn[randomEnemyIndex]; i++)
            {
                int randomIndex = Random.Range(0, m_spawnPoints.Length);
                Instantiate(m_enemyPrefab[randomEnemyIndex], m_spawnPoints[randomIndex].transform.position, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogError("SpawnPoints, EnemyPrefabs or NumberOfEnemiesToSpawn not set in EnemyManager.");
        }
    }
    
}