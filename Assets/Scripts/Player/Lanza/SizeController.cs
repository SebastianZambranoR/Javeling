using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour
{
    [SerializeField] float minXScale = 1;
    [SerializeField] float maxXScale = 3.2f;

    WallGrap wallGrap;
    JavelImpulse impulse;
    private void Awake()
    {
        wallGrap = GetComponentInChildren<WallGrap>();
    }

    public void GetReferences(JavelImpulse playerImpulse)
    {
        impulse = playerImpulse;
    }

    private void OnEnable()
    {
        wallGrap.hitEnemy += IncreaseSize;
    }
    private void OnDisable()
    {
        wallGrap.hitEnemy -= IncreaseSize;
    }
    public void IncreaseSize()
    {
        if (transform.localScale.x < maxXScale)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.7333333333333333f, transform.localScale.y, transform.localScale.z);
            impulse.impulseForce += 10;
        }

    }

    public void DecreaseSize()
    {
        if (transform.localScale.x > minXScale)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.7333333333333333f, transform.localScale.y, transform.localScale.z);
            impulse.impulseForce -= 10;
        }
    }
}
