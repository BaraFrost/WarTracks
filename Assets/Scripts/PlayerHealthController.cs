using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static int playerHealth;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (playerHealth <= 0)  
        {
            Destroy(gameObject);
        }
    }
}
