using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AmmoCrate : MonoBehaviour
{
    // Public attributes
    public GameObject item;

    public UnityEvent itemPickUp;


    // Private attributes
    private bool isFull = true;

    public void OpenAmmoCrate()
    {
        if (isFull && item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            itemPickUp.Invoke();
            isFull = false;
        }
    }
}
