using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class HeelCounter : MonoBehaviour
{
    public int heelCount;
    [SerializeField]
    private TMP_Text heelScale;
    


    // Update is called once per frame
    void Update()
    {
        heelCount = PlayerPrefs.GetInt("Heel");
        
        heelScale.text = heelCount.ToString();

        
    }
}
