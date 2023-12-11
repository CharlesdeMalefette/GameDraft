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
    public GameObject weaponHolder;

    // HUD
    public GameObject interactNotification;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();

        activeRenderer = smallRenderer;
        interactNotification = transform.Find("InteractNotification").gameObject;
    }

    private void Update()
    {
        print(weaponHolder);
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

    #region equipment

    public void EquipWeapon(GameObject weapon)
    {
        print("h");
        print(weaponHolder);
        weapon.transform.SetParent(weaponHolder.transform);
    }

    #endregion


}
