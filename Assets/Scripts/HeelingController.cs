using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeelingController : MonoBehaviour
{
    public int value;
    

    // Update is called once per frame
    void Update()
    {
        if(value<=0)
        {
            value = 0;
        }
    }
}
