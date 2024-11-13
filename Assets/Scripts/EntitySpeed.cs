using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpeed : MonoBehaviour
{
    public int value;
    public int normValue;
    // Start is called before the first frame update
    void Start()
    {
        normValue = value;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (value <= 0)
        {
            value = 1;
        }
    }
}
