using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CarMovement : MonoBehaviour
{

    public float speed;
    public int direction;


    void FixedUpdate()
    {
        transform.position += math.sign(direction) * Vector3.right * speed * Time.deltaTime;
    }
}
