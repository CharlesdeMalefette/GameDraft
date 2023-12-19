
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] walkSprites;

    public Sprite[] idleSprites;

    public Sprite[] jumpSprites;

    public float framerate = 1f / 6f;

    private SpriteRenderer spriteRenderer;

    private PlayerMovement playerMovement;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

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
        Sprite[] sprites = idleSprites;

        if (walkSprites.Length > 0)
        {
            sprites = walkSprites;
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
