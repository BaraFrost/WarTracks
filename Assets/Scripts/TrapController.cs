using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EntityHealth>(out var health))
        {
            health.value -= 100 ;
        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<EntitySpeed>(out var speed))
        {
            speed.value = speed.normValue;
        }
    }
}
