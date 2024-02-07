using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeonBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force = 10.0f;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        print("heyr");
        print(transform.rotation);
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = (transform.rotation.y * Vector2.left + transform.rotation.w * Vector2.right) * force;
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
        //print(other);
    }
}
