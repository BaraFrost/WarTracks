using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeelCounter : MonoBehaviour
{
    public int heelCount;
    [SerializeField]
    private TMP_Text heelScale;
    void Start()
    {
        heelCount = PlayerPrefs.GetInt("Heel");
    }


    // Update is called once per frame
    void Update()
    {
        heelCount = PlayerPrefs.GetInt("Heel");
        PlayerPrefs.SetInt("Heel", heelCount);
        PlayerPrefs.Save();
        heelScale.text = heelCount.ToString();
    }
}
