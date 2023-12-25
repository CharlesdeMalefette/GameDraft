
using System;
using Unity.VisualScripting;
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

    int lenAttackCounter = 0;

    private Rigidbody2D _rigidbody;

    private bool flip = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        punchAnimation = GetComponent<Animation>();
        _rigidbody = GetComponent<Rigidbody2D>();

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

        if (playerMovement.attack && !isAttacking)
        {
            isAttacking = true;

        }

        if (isAttacking)
        {
            sprites = punchSprites;

            spriteRenderer.sprite = sprites[lenAttackCounter];

            lenAttackCounter += 1;

            if (lenAttackCounter == punchSprites.Length - 1)
            {
                isAttacking = false;

                lenAttackCounter = 0;
            }

        }

        if (playerMovement.running)
        {
            sprites = walkSprites;
        }

        print(playerMovement.grounded);
        if (!playerMovement.grounded)
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

            if (Math.Abs(_rigidbody.velocity.x) < 0.001f)
            {
                spriteRenderer.flipX = flip;
            }
            else
            {
                flip = _rigidbody.velocity.x < 0;
                spriteRenderer.flipX = flip;
            }

        }
    }
}
