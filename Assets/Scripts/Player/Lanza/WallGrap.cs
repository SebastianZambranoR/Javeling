using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrap : MonoBehaviour
{
    HingeJoint2D joint;

    bool canGrap;
    bool isGrapping;
    bool wantToGrap;
    public bool isAttacking;
    public bool wallRelease;
    public bool CanGrap { get => canGrap; set => canGrap = value; }
    public bool IsGrapping { get => isGrapping; set => isGrapping = value; }

    public delegate void HitEnemyHandler();
    public event HitEnemyHandler hitEnemy;

    public delegate void TouchGround();
    public event TouchGround touchGround;

    [SerializeField] AudioClip Grapped;

    bool SoundPlayed;
    private void Awake()
    {
        joint = GetComponentInParent<HingeJoint2D>();
        canGrap = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            wantToGrap = true;
        }
        else
        {
            wantToGrap = false;
        }

        if(!wantToGrap && isGrapping)
        {
            joint.enabled = false;
            wallRelease = true;
            isGrapping = false;
            SoundPlayed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MuroVertical") && !isGrapping)
        {
            if (canGrap && wantToGrap)
            {
                joint.enabled = true;
                joint.connectedBody = collision.collider.GetComponent<Rigidbody2D>();
                joint.connectedAnchor = collision.transform.InverseTransformPoint(collision.GetContact(0).point);
                if (!SoundPlayed)
                {
                    PlaySound();
                    SoundPlayed = true;
                }
                isGrapping = true;
            }
        }

        if (collision.collider.CompareTag("Enemy") && isAttacking)
        {
            hitEnemy.Invoke();
            Debug.Log("incrementar lanza");
        }

        if (collision.collider.CompareTag("Ground"))
        {
            touchGround.Invoke();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MuroVertical") && !isGrapping)
        {
            if (canGrap && wantToGrap)
            {
                joint.enabled = true;
                joint.connectedBody = collision.collider.GetComponent<Rigidbody2D>();
                joint.connectedAnchor = collision.transform.InverseTransformPoint(collision.GetContact(0).point);
                if (!SoundPlayed)
                {
                    PlaySound();
                    SoundPlayed = true;
                }
                isGrapping = true;
            }
        }
    }

    void PlaySound()
    {
        AudioSource.PlayClipAtPoint(Grapped, transform.position);
    }

}
