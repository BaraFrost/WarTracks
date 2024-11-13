using BrewedInk.CRT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerHealth : MonoBehaviour
{
    public CRTData data1;
    [SerializeField]
    private float scale;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scale = data1.pixelationAmount;
    }
}
