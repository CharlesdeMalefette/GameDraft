using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float damage;

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Ennemy"))
        {
            collider.gameObject.GetComponent<HealthManagement>().Hit(damage);
        }
        Destroy(gameObject);
    }
}
