using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaLanza : MonoBehaviour
{
    Rigidbody2D rb;
    HingeJoint2D worldPivot;
    [SerializeField] float launchForce;

    AngleLimiter limiter;

    [SerializeField] float minXScale = 0.7f;
    [SerializeField] float maXScale = 3.2f;
    [HideInInspector]public bool canImpulse;
    bool canGrapp;
    bool enganchado;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        worldPivot = GetComponent<HingeJoint2D>();
        limiter = GetComponentInChildren<AngleLimiter>();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Fire1") && !enganchado && canImpulse)
        {
            rb.velocity = Vector2.zero;
            rb.mass = 0.3f;
            rb.AddForce((Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized * launchForce, ForceMode2D.Impulse);
            canImpulse = false;
        }

        if (Input.GetButton("Fire2"))
        {
            canGrapp = true;
        }
        else
        {
            canGrapp = false;
        }
        
        
        if(enganchado && Input.GetButtonUp("Fire2"))
        {
            canGrapp = false;
            worldPivot.enabled = false;
            Invoke("Desengacharse", 0.3f);
        }

    }



    void Desengacharse()
    {
        enganchado = false;
        rb.mass = 0.001f;
    }

    public void TestGrap(Collision2D collision)
    {
        if(!collision.collider.CompareTag("MuroVertical"))
            canImpulse = true;
        if (canGrapp)
        {
            if (collision.collider.CompareTag("Enganchable"))
            {
                if (!enganchado)
                {
                    worldPivot.enabled = true;
                    worldPivot.connectedBody = collision.collider.GetComponent<Rigidbody2D>();
                    worldPivot.connectedAnchor = collision.transform.InverseTransformPoint(collision.GetContact(0).point);
                    enganchado = true;
                }
            }
        }
    }

    public void IncreaseSize()
    {
        if(transform.localScale.x < maXScale)
            transform.localScale = new Vector3(transform.localScale.x + 0.625f, transform.localScale.y, transform.localScale.z);
    }

    void ReduceSize()
    {
        if(transform.localScale.z > minXScale)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.625f, transform.localScale.y, transform.localScale.z);
        }
    }
}
