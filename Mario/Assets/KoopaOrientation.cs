using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaOrientation : MonoBehaviour
{

    private GameObject player;

    private Vector2 previousDirection;

    private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bullet = GetComponent<EnemyShooting>().bullet;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.transform.position - transform.position;

        if (direction.x * previousDirection.x < 0)
        {
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 180f);
            bullet.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 180f);
        }

        previousDirection = direction;
    }
}
