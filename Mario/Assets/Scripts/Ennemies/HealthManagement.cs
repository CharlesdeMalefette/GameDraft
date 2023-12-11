using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManagement : MonoBehaviour
{
    public float health;

    private float currentHealth;

    private void Start()
    {
        currentHealth = health;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
    }
}
