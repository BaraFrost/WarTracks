using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtelleryBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    Rigidbody2D rb;
    private SpriteRenderer sprite;
    public bool moveRight;
    [SerializeField]
    private int damage;
    public float lifeTime;
    public float minAngle;  // минимальный угол в градусах
    public float maxAngle;  // максимальный угол в градусах
    [SerializeField]
    private GameObject smoke;
    private EntityHealth entityHealth;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomAngle = UnityEngine.Random.Range(minAngle, maxAngle);
        //rb.velocity = (moveRight ? transform.right : Quaternion.AngleAxis(180, Vector3.forward) * transform.right) * speed;
        Vector3 direction = Quaternion.AngleAxis(randomAngle, Vector3.forward) * transform.right;
        rb.velocity = direction * speed;

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            Instantiate(smoke, transform.position, transform.rotation);
        }

        if (collision.gameObject.TryGetComponent<EntityHealth>(out var health))
        {

            health.value = health.value - damage;


            Destroy(gameObject);
            Instantiate(smoke, transform.position, transform.rotation);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
            Instantiate(smoke, transform.position, transform.rotation);
        }
    }
    public void Update()
    {

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
            
        }
        RotateTowardsMovementDirection(rb.velocity);
    }
   
    void RotateTowardsMovementDirection(Vector2 movement)
    {
        // ¬ычисл€ем угол в радианах от вектора движени€
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        // ѕоворачиваем объект по оси Z (в 2D пространстве)
        rb.rotation = angle;
    }
}
