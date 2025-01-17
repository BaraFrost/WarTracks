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
        // Установка случайного значения для spawnInterval


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
