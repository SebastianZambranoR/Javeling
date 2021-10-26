using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelImpulse : MonoBehaviour
{
    Rigidbody2D rb;
    WallGrap wallGrap;
    IndicatorManager indicator;
    public float impulseForce;

    float holdTime;
    int holdMaxTime = 4;
    bool prepareAttack;
    bool isAttacking;
    bool blockAttackInput;

    [HideInInspector]public bool inAir;
    public bool IsAttacking { get => isAttacking; }

    public void GetReferences(Rigidbody2D playerRb, WallGrap playerWallGrap, IndicatorManager playerIndicator)
    {
        rb = playerRb;
        wallGrap = playerWallGrap;
        indicator = playerIndicator;
        indicator.holdMaxTime = holdMaxTime;
    }


    // Update is called once per frame
    void Update()
    {
        wallGrap.isAttacking = isAttacking;
        if (Input.GetButton("Fire1") && !blockAttackInput && !isAttacking)
        {
            holdTime += Time.deltaTime;
            prepareAttack = true;
        }

        indicator.holdTime = holdTime;
        if(holdTime > holdMaxTime)
        {
            prepareAttack = false;
            blockAttackInput = true;
        }

        if(Input.GetButtonUp("Fire1") && !prepareAttack && !isAttacking)
        {
            blockAttackInput = false;
            holdTime = 0;
        }

        if (Input.GetButtonUp("Fire1") && prepareAttack)
        {
            if (!isAttacking)
            {
                PerformAttack();
                holdTime = 0;
                isAttacking = true;
            }
        }


    }

    void PerformAttack()
    {
        rb.velocity = Vector2.zero;
        rb.mass = 0.3f;
        rb.AddForce(transform.right * (impulseForce * GetForceMultiplier()),ForceMode2D.Impulse);
    }

    public void AttackReset()
    {
        holdTime = 0;
        rb.mass = 0.0001f;
        prepareAttack = false;
        blockAttackInput = false;
        isAttacking = false;
    }

    float GetForceMultiplier()
    {
        float multiplier = 1;
        if (!inAir)
        {
            if (holdTime <= 1)
            {
                multiplier = 1;
            }
            else if (holdTime > 1 && holdTime <= 2)
            {
                multiplier = 1.5f;
            }
            else if (holdTime > 2)
            {
                multiplier = 2f;
            }
        }
        else
        {
            multiplier = 2f;
        }
     

        return multiplier;
    }
}
