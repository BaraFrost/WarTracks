using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerejablMove : MonoBehaviour
{
    [SerializeField]
    private EntitySpeed speed;
    [SerializeField]
    private GameObject bombSpawn;
    [SerializeField] 
    private GameObject objectToSpawn;
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float lifeTime;
    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        Vector2 position = transform.position;

        position += Vector2.left * speed.value * Time.deltaTime;

        transform.position = position;

        
        timer-=Time.deltaTime;
        if(timer<=0)
        {
            Instantiate(objectToSpawn, bombSpawn.transform.position, transform.rotation);
              timer = spawnInterval;

        }

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);

        }
    }
}
