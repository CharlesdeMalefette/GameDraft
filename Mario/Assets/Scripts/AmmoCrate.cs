using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    // Public attributes
    public GameObject item;
    public bool interactive;
    //DEBUG debug only mode


    // Private attributes
    private bool interact = true;

    public void Interact()
    {
        if (interact && item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            interact = false;
        }

    }
}
