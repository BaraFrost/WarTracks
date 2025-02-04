using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class ThisLevel : MonoBehaviour
{
    [SerializeField]
    private int nextSceneNumber;//номер следующей сцены
    void Start()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedLevel", activeSceneIndex);
        PlayerPrefs.Save();
        nextSceneNumber = activeSceneIndex + 1;

        YG2.MetricaSend("coin_balance", new Dictionary<string, string>
        {
            { "active_scene", activeSceneIndex.ToString() }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
