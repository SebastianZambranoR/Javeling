using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToMouse : MonoBehaviour
{
    Rigidbody2D rb;
    [HideInInspector]public bool canPoint;
    public void GetReferences(Rigidbody2D playerRb)
    {
        rb = playerRb;
    }

    private void FixedUpdate()
    {
        if (canPoint)
        {
            var mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            rb.MoveRotation(angle);
        }
    }
}
