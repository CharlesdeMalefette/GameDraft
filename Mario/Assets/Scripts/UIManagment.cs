using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagment : MonoBehaviour
{

    public Sprite[] healthSprites;
    public Sprite[] weaponSprites;

    private Image healthImage;
    private Image weaponImage;
    private int HP;
    private int weapon;

    void Start()
    {
        Transform health = transform.Find("Health");
        Transform weapon = transform.Find("Weapon");

        healthImage = health.GetComponent<Image>();
        weaponImage = weapon.GetComponent<Image>();
        HP = 0;
    }

    public void decreaseHP()
    {
        HP++;
        if (HP < healthSprites.Length)
        {
            healthImage.sprite = healthSprites[HP];
        }
    }

    public void switchWeapon()
    {
        weapon++;
        if (weapon >= weaponSprites.Length) { weapon = 0; }
        weaponImage.sprite = weaponSprites[weapon];
    }
}
