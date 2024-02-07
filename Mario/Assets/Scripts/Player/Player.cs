using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    // Life
    public float HP = 5;
    public UnityEvent hit;
    public UnityEvent death;
    public UnityEvent switchWeapon;
    private bool dead = false;

    // Equipment
    //public GameObject weaponHolder;

    // HUD
    public GameObject interactNotification;

    private void Awake()
    {

        //Equipement & Fight
        //interactNotification = transform.Find("InteractNotification").gameObject;
        ownWeapon = false;
        //weaponHolder = transform.Find("WeaponHolder").gameObject;
        nextAttackFistTime = Time.time;
        nextAttackRifleTime = Time.time;
        bulletPos = transform.Find("BulletPos");
    }

    #region Health

    public void Hit()
    {
        if (!dead && HP == 0)
        {
            Debug.Log("Mort");
            death.Invoke();
        }
        else
        {
            hit.Invoke();
            HP--;
        }
    }

    private void Death()
    {
        //GameManager.Instance.ResetLevel(3f);
    }

    #endregion

    #region UI
    public void notifyPlayer()
    {
        interactNotification.SetActive(true);
    }
    public void deNotifyPlayer()
    {
        interactNotification.SetActive(false);
    }

    #endregion

    #region Equipment & Fight

    // Weapon
    public bool ownWeapon;
    public bool hasWeaponInHand;
    public Transform bulletPos;
    public GameObject bullet;

    // Fistfight
    public Transform attackPoint;

    public float attackRange;
    public LayerMask ennemyLayer;
    public float fistDamage;
    public float attackFistRate;
    private float nextAttackFistTime;

    // Rifle
    public float attackRifleRate;
    private float nextAttackRifleTime;

    public void PickUpWeapon()
    {
        ownWeapon = true;
        hasWeaponInHand = true;
        switchWeapon.Invoke();
    }

    public void EquipWeapon()
    {
        hasWeaponInHand = true;
        switchWeapon.Invoke();
    }

    public void DesequipWeapon()
    {
        hasWeaponInHand = false;
        switchWeapon.Invoke();
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
        if (Time.time > nextAttackRifleTime)
        {
            nextAttackRifleTime = Time.time + 1 / attackRifleRate;
            Instantiate(bullet, bulletPos.position, transform.rotation);
        }
    }

    public void FistAttack()
    {
        if (Time.time > nextAttackFistTime)
        {
            nextAttackFistTime = Time.time + 1 / attackFistRate;
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
