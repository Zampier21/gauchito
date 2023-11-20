using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaAI : MonoBehaviour
{

    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    public float maxHealth = 100;
    public float currentHealth;
    public GameObject prefabOnDeath;

    public void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float dano)
    {
        currentHealth -= dano;
        if(currentHealth <= 0)
        {
            Death();
        }
    }

    public void Update()
    {

    }

    public void Death()
    {
        Instantiate(prefabOnDeath,transform.position, transform.rotation);
        Destroy(gameObject);

        if(OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
    }
}
