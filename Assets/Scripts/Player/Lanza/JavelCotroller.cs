using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelCotroller : MonoBehaviour
{
    Rigidbody2D rb;

    PointToMouse pointing;
    JavelImpulse impulseManager;
    WallGrap wallGrap;
    SizeController size;
    IndicatorManager indicator;

    float holdDecreaseTime = 3f;
    bool decreased;
    [HideInInspector]public bool wallInFront;
    bool inAir;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pointing = GetComponent<PointToMouse>();
        wallGrap = GetComponentInChildren<WallGrap>();
        impulseManager = GetComponent<JavelImpulse>();
        size = GetComponentInChildren<SizeController>();
        indicator = GetComponent<IndicatorManager>();
        size.GetReferences(impulseManager);
        pointing.GetReferences(rb);
        impulseManager.GetReferences(rb, wallGrap, indicator);

    }

    private void OnEnable()
    {
        wallGrap.touchGround += DisableMousePointing;
    }

    private void OnDisable()
    {
        wallGrap.touchGround -= DisableMousePointing;
    }

    private void Update()
    {
        impulseManager.inAir = inAir;
        if(rb.velocity.y < 0 && impulseManager.IsAttacking && !wallGrap.IsGrapping)
        {
            if (!inAir)
            {
                impulseManager.AttackReset();
            }
        }

        if (wallGrap.wallRelease)
        {
            impulseManager.Invoke("AttackReset",0.1f);
            wallGrap.wallRelease = false;
        }

        if (Input.GetButton("Fire2") && !wallGrap.IsGrapping && !inAir && !decreased)
        {
            holdDecreaseTime -= Time.deltaTime;
            if(holdDecreaseTime < 0)
            {
                size.DecreaseSize();
                decreased = true;
            }
        }

        if(Input.GetButtonUp("Fire2") && decreased)
        {
            holdDecreaseTime = 3f;
            decreased = false;
        }

        if(!pointing.canPoint && !inAir)
        {
            pointing.canPoint = true;
        }

        if (wallInFront)
        {
            rb.mass = 0.3f;
        }
        else if(!impulseManager.IsAttacking && !wallGrap.IsGrapping)
        {
            rb.mass = 0.0001f;
        }
    }

    public void InAir(bool groundState)
    {
        inAir = groundState;
    }

    void DisableMousePointing()
    {
        pointing.canPoint = false;
    }

    /* falta vincular el incrementador y decrementador, falta establecer los indicativos del sistema
     * de carga, falta aplicar la vida del personaje*/
}
