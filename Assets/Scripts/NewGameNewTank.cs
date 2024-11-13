using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameNewTank : MonoBehaviour
{
    [SerializeField]
    private ChooseTank tank1;
    [SerializeField]
    private GameObject butPlay;
    [SerializeField]
    private GameObject butAngar;
    // Start is called before the first frame update
    void Start()
    {
        if (tank1.tankID== 0)
        {
            butPlay.SetActive(false);
            butAngar.SetActive(false);
        }
        else
        {
            butPlay.SetActive(true);
            butAngar.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
