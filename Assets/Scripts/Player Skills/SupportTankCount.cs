using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SupportTankCount : MonoBehaviour
{
    public int sTCount;
    [SerializeField]
    private TMP_Text sTScale;
    void Start()
    {
        sTCount = PlayerPrefs.GetInt("ST");
    }


    // Update is called once per frame
    void Update()
    {
        sTCount = PlayerPrefs.GetInt("ST");
        PlayerPrefs.SetInt("ST", sTCount);
        PlayerPrefs.Save();
        sTScale.text = sTCount.ToString();
    }
}
