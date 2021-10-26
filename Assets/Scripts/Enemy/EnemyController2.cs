using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2 : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] LayerMask playerLayer;
    ContactFilter2D filter = new ContactFilter2D();

    GameObject player;
    Collider2D[] col = new Collider2D[5];

    [SerializeField] float MoveSpeed;
    [SerializeField] GameObject wayPoint1;
    [SerializeField] GameObject wayPoint2;
    GameObject nextWayPoint;
    [SerializeField] AudioClip clonkSound;
    private void Start()
    {
        wayPoint1.transform.SetParent(null);
        wayPoint2.transform.SetParent(null);
        nextWayPoint = wayPoint1;
        filter.SetLayerMask(playerLayer);
    }


    // Update is called once per frame
    void Update()
    {
        
        Collider2D sphere = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        if(sphere)
            sphere.OverlapCollider(filter, col);

        if(col[0]!= null)
        {
            player = col[0].gameObject;
            Vector2 playerVector = transform.position - player.transform.position;
            if (playerVector.normalized.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (playerVector.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, nextWayPoint.transform.position, MoveSpeed);

        if(Vector2.Distance(transform.position, nextWayPoint.transform.position) <= 0){
            if(nextWayPoint == wayPoint1)
            {
                nextWayPoint = wayPoint2;
            }
            else
            {
                nextWayPoint = wayPoint1;
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PuntaLanza"))
        {
            AudioSource.PlayClipAtPoint(clonkSound, transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
