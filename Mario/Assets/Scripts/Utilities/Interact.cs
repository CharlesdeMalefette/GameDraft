using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    private bool notifyPlayerOnInteractible;

    void Start()
    {
        notifyPlayerOnInteractible = true;
    }

    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
                notifyPlayerOnInteractible = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (notifyPlayerOnInteractible)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                isInRange = true;
                collider.gameObject.GetComponent<Player>().notifyPlayer();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            collider.gameObject.GetComponent<Player>().deNotifyPlayer();
        }
    }
}
