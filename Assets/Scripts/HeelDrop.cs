using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeelDrop : MonoBehaviour
    
{
    [SerializeField]
    private GameObject heeling;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DropHeel()
    {
        Random.Range(0, 1);
        Instantiate(heeling);
    }
}
