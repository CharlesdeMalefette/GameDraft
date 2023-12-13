using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

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

        //Equipement & Fight
        interactNotification = transform.Find("InteractNotification").gameObject;
        ownWeapon = false;
        weaponHolder = transform.Find("WeaponHolder").gameObject;
        nextAttackTime = Time.time;
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

    #region Equipment & Fight

    // Weapon
    public bool ownWeapon;
    public bool hasWeaponInHand;

    // Fistfight
    public Transform attackPoint;
    public float attackRange;
    public LayerMask ennemyLayer;
    public float fistDamage;
    public float attackRate;
    private float nextAttackTime;

    public void PickUpWeapon()
    {
        ownWeapon = true;
        hasWeaponInHand = true;
        weaponHolder.SetActive(true);
    }

    public void EquipWeapon()
    {
        hasWeaponInHand = true;
        weaponHolder.SetActive(true);
    }

    public void DesequipWeapon()
    {
        hasWeaponInHand = false;
        weaponHolder.SetActive(false);
    }

    public void Attack()
    {
        if (hasWeaponInHand)
        {
            WeaponAttack();
        }
        else
        {
            FistAttack();
        }
    }

    public void WeaponAttack()
    {
        weaponHolder.GetComponent<Weapon>().Attack();
    }

    public void FistAttack()
    {
        if (Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + 1 / attackRate;
            Collider2D[] ennemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, ennemyLayer);
            foreach (Collider2D col in ennemiesHit)
            {
                col.gameObject.GetComponent<HealthManagement>().Hit(fistDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    #endregion
}
