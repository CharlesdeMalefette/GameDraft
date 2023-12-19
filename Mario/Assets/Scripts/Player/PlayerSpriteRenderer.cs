
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{

    public SpriteRenderer spriteRenderer { get; private set; }
    private PlayerMovement movement;

    public AnimatedSprite idle;
    public Sprite jump;
    public AnimatedSprite run;
    public Sprite slide;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {

        spriteRenderer.enabled = false;
    }

    private void LateUpdate()
    {
        run.enabled = movement.running;



        if (movement.jumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (movement.sliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if (!movement.running)
        {
            idle.enabled = true;
        }
    }
}
