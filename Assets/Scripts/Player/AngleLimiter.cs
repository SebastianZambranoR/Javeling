using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleLimiter : MonoBehaviour
{
    public bool TouchingGround;
    public bool CanRotate;
    [SerializeField] GameObject lanza;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);

        if(Vector2.Angle(transform.right, lanza.transform.right) >= 90)
        {
            CanRotate = false;
        }
        else
        {
            CanRotate = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            TouchingGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            TouchingGround = false;
        }
    }
}
