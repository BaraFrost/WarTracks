using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyPlaneController : MonoBehaviour
{

    [SerializeField]
    private EntitySpeed speed;




    [SerializeField]
    private float lifeTime;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        // Установка случайного значения для spawnInterval
        if (player != null)
        {
            speed = player.GetComponent<EntitySpeed>();
        }

    }

    void Update()
    {
        Vector2 position = transform.position;

        position += Vector2.right * speed.value * Time.deltaTime;

        transform.position = position;


        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
