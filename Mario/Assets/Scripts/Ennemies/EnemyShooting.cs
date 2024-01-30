using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{

    public GameObject bullet;
    public Transform bulletPos;
    public Sprite shoot;
    private Sprite idle;
    private float timer;

    private GameObject player;
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        idle = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 30)
        {

            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                StartCoroutine(Shoot());
            }
        }


    }

    IEnumerator Shoot()
    {
        spriteRenderer.sprite = shoot;
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);

        spriteRenderer.sprite = idle;
    }
}
