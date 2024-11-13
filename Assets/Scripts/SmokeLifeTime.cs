using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeLifeTime : MonoBehaviour
{
    [SerializeField]
    private float smokeLifeTime = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        smokeLifeTime -= Time.deltaTime;
        if (smokeLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
