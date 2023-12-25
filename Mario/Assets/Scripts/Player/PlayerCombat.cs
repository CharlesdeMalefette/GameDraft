using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComabt : MonoBehaviour
{

    public List<AttackSO> combo;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Punch1"))
        {
            Attack();
        }
        ExitAttack();

    }

    void Attack()
    {
        anim.Play("Attack", 0, 0);
    }

    void ExitAttack()
    {

    }

    void EndCombo()
    {

    }
}
