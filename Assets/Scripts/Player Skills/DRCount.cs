using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class DRCount : MonoBehaviour
{
    public int dRCount;
    [SerializeField]
    private TMP_Text dRScale;
    void Start()
    {
        dRCount = PlayerPrefs.GetInt("DR");
    }


    // Update is called once per frame
    void Update()
    {
        dRCount = PlayerPrefs.GetInt("DR");
       
        dRScale.text = dRCount.ToString();

     
    }
}
