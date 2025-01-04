using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyDeadRacer : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private GameObject smoke;

    [SerializeField]
    private float lifeTime;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Instantiate(smoke, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.TryGetComponent<EntityHealth>(out var health))
            {

                health.value = health.value - damage;
            }

            Instantiate(smoke, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
