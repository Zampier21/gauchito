using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaAI : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;


    public void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float dano)
    {
        currentHealth -= dano;
        Debug.Log(currentHealth);
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
        Destroy(gameObject);
    }
}
