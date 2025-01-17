using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaneCount : MonoBehaviour
{
    public int planeCount;
    [SerializeField]
    private TMP_Text planeScale;
    void Start()
    {
        planeCount = PlayerPrefs.GetInt("Plane");
    }


    // Update is called once per frame
    void Update()
    {
        planeCount = PlayerPrefs.GetInt("Plane");
        PlayerPrefs.SetInt("Plane", planeCount);
        PlayerPrefs.Save();
        planeScale.text = planeCount.ToString();
    }
}
