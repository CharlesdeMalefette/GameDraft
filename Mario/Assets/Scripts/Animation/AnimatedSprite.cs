
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] walkSprites;

    public Sprite[] idleSprites;

    public Sprite[] jumpSprites;
    public Sprite[] punchSprites;

    private Animation punchAnimation;

    public float framerate = 1f / 6f;

    private SpriteRenderer spriteRenderer;

    private PlayerMovement playerMovement;
    private int frame;
    private bool isAttacking = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        punchAnimation = GetComponent<Animation>();

    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {

        if (playerMovement.attack && !isAttacking)
        {
            // Set the attacking flag to true to prevent re-triggering the animation
            isAttacking = true;

            // Play the punch animation
            //punchAnimator.enabled = true;
            punchAnimation.Play();
        }

        Sprite[] sprites = idleSprites;

        if (playerMovement.running)
        {
            sprites = walkSprites;
        }

        if (playerMovement.jumping)
        {
            sprites = jumpSprites;
        }

        frame++;
        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }
    }
}
