using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombCounter : MonoBehaviour
{
    public int bombCount;
    [SerializeField]
    private TMP_Text bombScale;
    void Start()
    {
        bombCount = PlayerPrefs.GetInt("Bomb");
    }

    
    // Update is called once per frame
    void Update()
    {
        bombCount = PlayerPrefs.GetInt("Bomb");
        PlayerPrefs.SetInt("Bomb", bombCount);
        PlayerPrefs.Save();
        bombScale.text = bombCount.ToString();
    }
}
