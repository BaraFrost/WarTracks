using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    Rigidbody2D rb;
    private SpriteRenderer sprite;
    public bool moveRight;
    [SerializeField]
    private int damage;
    
    private EntityHealth entityHealth;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = (moveRight ? transform.right : Quaternion.AngleAxis(180, Vector3.forward) * transform.right) * speed;

       
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

       

        if (collision.gameObject.GetComponent<EntityHealth>())
        {

            entityHealth.value = entityHealth.value-damage;
            Destroy(gameObject);

        }
    }

}
