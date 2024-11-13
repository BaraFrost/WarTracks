using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
