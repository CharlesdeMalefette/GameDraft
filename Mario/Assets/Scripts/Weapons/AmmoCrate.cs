using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    // Public attributes
    public GameObject item;


    // Private attributes
    private bool isFull = true;

    public void OpenAmmoCrate()
    {
        if (isFull && item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            isFull = false;
        }

    }
}
