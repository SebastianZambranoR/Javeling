using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] ParticleSystem enemyDeath;
    Rigidbody2D rb;
    [SerializeField] float Impulse;
    [SerializeField] float maxYVelocity;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float minPos, maxPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        minPos = rb.position.y - 0.5f;
        maxPos = rb.position.y;
    }
    private void Update()
    {
        rb.position = new Vector2(transform.position.x, Mathf.Clamp(rb.position.y, minPos, maxPos));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PuntaLanza") && collision.collider.GetComponentInParent<JavelImpulse>().IsAttacking)
        {
            EnemyDeath();
            Destroy(gameObject);
        }
    }

    void EnemyDeath()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Instantiate(enemyDeath, transform.position, Quaternion.identity);
    }

    void Elevate()
    {
        //rb.AddForce(Vector2.up * Impulse, ForceMode2D.Impulse);
        rb.velocity = new Vector2(0, 2.5f);
    }
}
