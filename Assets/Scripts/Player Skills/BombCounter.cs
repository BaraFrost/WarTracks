using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class BombCounter : MonoBehaviour
{
    public int bombCount;
    [SerializeField]
    private TMP_Text bombScale;
   

    
    // Update is called once per frame
    void Update()
    {
        bombCount = PlayerPrefs.GetInt("Bomb");
        
        bombScale.text = bombCount.ToString();

        
    }
}
