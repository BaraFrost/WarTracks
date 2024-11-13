using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heeling : MonoBehaviour
{
    [SerializeField]
    private float heelCount;
    [SerializeField]
    private GameObject player;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.TryGetComponent<EntityHealth>(out var health))
        {
            health.value += heelCount;
            Destroy(gameObject);
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
