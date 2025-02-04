using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class ActiveButtonPlay : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPlay;
    [SerializeField]
    private int tankI;
    [SerializeField]
    private Image BackGrin;
    [SerializeField]
    private Image BackBlue;
    [SerializeField]
    private float ind;
    void Start()
    {
        buttonPlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        tankI = PlayerPrefs.GetInt("TankID");
        if ( tankI >=1)
        {
            buttonPlay.SetActive(true);
        }
       if(tankI==1)
       {
            BackBlue.fillAmount +=ind*Time.deltaTime;
            BackGrin.fillAmount -=ind * Time.deltaTime;
       }
       else if(tankI==2)
        {
            BackGrin.fillAmount +=ind * Time.deltaTime;
            BackBlue.fillAmount -= ind * Time.deltaTime;
        }
    }
}
