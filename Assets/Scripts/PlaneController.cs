using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField]
    private EntitySpeed speed;
    
    
    
    
    [SerializeField]
    private float lifeTime;

    void Start()
    {
        // ��������� ���������� �������� ��� spawnInterval
        
        
    }

    void Update()
    {
        Vector2 position = transform.position;

        position += Vector2.left * speed.value * Time.deltaTime;

        transform.position = position;


        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
