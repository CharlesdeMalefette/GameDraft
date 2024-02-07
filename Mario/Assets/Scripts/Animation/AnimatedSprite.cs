
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class SpriteCollection
{
    public Sprite[] walk;
    public Sprite[] idle;
    public Sprite[] jump;
    public Sprite[] attack;
    public Sprite[] crouch;
}


public class AnimatedSprite : MonoBehaviour
{
    public SpriteCollection normalSpriteCollections;
    public SpriteCollection shootSpriteCollections;

    public float framerate = 1f / 6f;

    private SpriteRenderer spriteRenderer;

    private PlayerMovement playerMovement;
    private Player player;
    private int frame;
    private bool isAttacking = false;
    private bool isCrouched = false;

    int lenAttackCounter = 0;

    private Rigidbody2D _rigidbody;

    private bool flip = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();

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
        SpriteCollection spriteCollection = normalSpriteCollections;

        if (player.hasWeaponInHand)
        {
            spriteCollection = shootSpriteCollections;
        }

        Sprite[] sprites = spriteCollection.idle;

        AttackAnimation(ref sprites, spriteCollection);

        if (playerMovement.crouched)
        {
            sprites = spriteCollection.crouch;

            if (!isCrouched)
            {
                isCrouched = true;
                spriteRenderer.sprite = sprites[0];
            }
            else
            {
                spriteRenderer.sprite = sprites[1];
            }
            return;
        }
        else
        {
            isCrouched = false;
        }

        if (playerMovement.running)
        {
            sprites = spriteCollection.walk;
        }

        if (!playerMovement.grounded)
        {
            sprites = spriteCollection.jump;
        }

        frame++;
        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];

            // if (Math.Abs(_rigidbody.velocity.x) < 0.001f)
            // {
            //     spriteRenderer.flipX = flip;
            // }
            // else
            // {
            //     flip = _rigidbody.velocity.x < 0;
            //     spriteRenderer.flipX = flip;
            // }

        }
    }

    private void AttackAnimation(ref Sprite[] sprites, SpriteCollection spriteCollection)
    {
        if (playerMovement.attack && !isAttacking)
        {
            isAttacking = true;
        }

        if (isAttacking)
        {
            sprites = spriteCollection.attack;

            spriteRenderer.sprite = sprites[lenAttackCounter];

            lenAttackCounter += 1;

            if (lenAttackCounter == sprites.Length - 1)
            {
                isAttacking = false;

                lenAttackCounter = 0;
            }
            return;
        }
    }

    private void CrouchAnimation(ref Sprite[] sprites, SpriteCollection spriteCollection)
    {
        if (playerMovement.crouched)
        {
            sprites = spriteCollection.crouch;

            if (!isCrouched)
            {
                isCrouched = true;
                spriteRenderer.sprite = sprites[0];
            }
            else
            {
                spriteRenderer.sprite = sprites[1];
            }
            return;
        }
        else
        {
            isCrouched = false;
        }
    }

}
