using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public void Attack()
    {
        print($"{transform.name}");
    }
}
