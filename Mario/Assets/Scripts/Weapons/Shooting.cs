using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;
    public Transform shootPoint;

    public void Shoot()
    {
        GameObject bulletIns = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        bulletIns.GetComponent<Rigidbody2D>().AddForce(Vector3.right * bulletSpeed);
    }
}
