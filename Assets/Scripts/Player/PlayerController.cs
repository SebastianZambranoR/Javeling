using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    JavelCotroller lanza;
    int state;
    //State: 0 = grounded (se puede disparar y se puede agarrar)
    //State: 1 = flying (se puede disparar 1 vez y se puede agarrar)
    //State: 2 = Grapping (se puede soltar)

    bool inAir;
    bool wallInFront;
    [SerializeField]Vector2 rayGroundOffSet;
    [SerializeField] float rayGroundLeght;
    [SerializeField] Vector2 rayWallOffSet;
    [SerializeField] float rayWallLeght;

    [SerializeField] LayerMask groundlayer;
    [SerializeField] ParticleSystem deathParticles;

    public delegate void OnPlayerDeath();
    public event OnPlayerDeath playerDeath;

    [SerializeField] AudioClip deathClip;
    private void Awake()
    {
        lanza = GetComponentInChildren<JavelCotroller>();
    }

    private void Update()
    {
        GroudCheck();
        WallDetector();
    }

    void GroudCheck()
    {
        RaycastHit2D ray = Physics2D.Raycast((Vector2)transform.position + rayGroundOffSet, Vector2.right, rayGroundLeght, groundlayer);

        if (ray)
        {
            inAir = false;
        }
        else
        {
            inAir = true;
        }
        lanza.InAir(inAir);
    }

    void WallDetector()
    {
        RaycastHit2D ray = Physics2D.Raycast((Vector2)transform.position + rayWallOffSet, Vector2.right, rayWallLeght, groundlayer);

        if (ray)
        {
            wallInFront = true;
        }
        else
        {
            wallInFront = false;
        }
        lanza.wallInFront = wallInFront;
    }


    private void OnDrawGizmos()
    {
        Debug.DrawRay((Vector2)transform.position + rayGroundOffSet, Vector2.right * rayGroundLeght);

        Debug.DrawRay((Vector2)transform.position + rayWallOffSet, Vector2.right * rayWallLeght);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Pincho") || collision.collider.CompareTag("Enemy"))
        {
            PlayerDeath();
        }
    }


    void PlayerDeath()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
        playerDeath.Invoke();
        this.gameObject.SetActive(false);
    }


}
