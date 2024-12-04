using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedReacerController : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private GameObject smoke;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
