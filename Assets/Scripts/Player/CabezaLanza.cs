using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabezaLanza : MonoBehaviour
{
    PruebaLanza lanza;

    private void Awake()
    {
        lanza = GetComponentInParent<PruebaLanza>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        lanza.TestGrap(collision);
    }
}
