using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AmmoCrate : MonoBehaviour
{
    // Public attributes
    public GameObject item;

    public UnityEvent itemPickUp;

    public Sprite openCrateSprite;


    // Private attributes
    private bool isFull = true;
    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OpenAmmoCrate()
    {
        if (isFull && item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            itemPickUp.Invoke();
            isFull = false;
            spriteRenderer.sprite = openCrateSprite;
        }
    }
}
