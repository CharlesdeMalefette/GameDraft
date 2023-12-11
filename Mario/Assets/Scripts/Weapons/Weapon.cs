using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Weapon : MonoBehaviour
{

    public UnityEvent OnAttack;

    public float fireRate;
    private float nextFireTime;

    private void Start()
    {
        nextFireTime = Time.time;
    }


    public void Attack()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + 1 / fireRate;
            OnAttack.Invoke();
        }
    }
}
