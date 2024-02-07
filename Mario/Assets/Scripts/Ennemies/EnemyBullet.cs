using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rb;
    public float force = 10.0f;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.left * force;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (other.gameObject.layer == 0 || other.gameObject.layer == 3)
        {
            Destroy(gameObject);
            if (other.gameObject.CompareTag("Player"))
            {
                player.Hit();
            }
        }
    }
}
