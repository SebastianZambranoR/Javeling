using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    public delegate void OnGameFinish();
    public event OnGameFinish finishGame;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("PuntaLanza"))
        {
            Instantiate(particles, transform.position, Quaternion.identity);
            finishGame.Invoke();
            Destroy(gameObject);
        }

    }
}
