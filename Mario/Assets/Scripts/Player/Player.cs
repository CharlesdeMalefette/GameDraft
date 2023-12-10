using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    // Sprites
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    // Animation
    private DeathAnimation deathAnimation;

    // Life
    public float HP = 5;
    public bool dead => deathAnimation.enabled;

    // Equipment
    public Weapon weapon;

    // HUD
    public GameObject interactNotification;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        weapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        activeRenderer = smallRenderer;
        interactNotification = transform.Find("InteractNotification").gameObject;
    }

    public void Hit()
    {
        if (!dead && HP == 0)
        {
            Death();
        }
        else
        {
            HP--;
        }
    }

    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }

    public void notifyPlayer()
    {
        interactNotification.SetActive(true);
    }
    public void deNotifyPlayer()
    {
        interactNotification.SetActive(false);
    }
}
