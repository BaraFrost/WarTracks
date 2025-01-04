using Infrastructure;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneLoader : SingletonCrossScene<GameSceneLoader>
{
    
    public void LoadSceneAfterDelay(int sceneNumber,float delay)
    {
        StartCoroutine(LoadSceneAfterDelayCoroutine(sceneNumber, delay)); // 0.5 ������� ��������
    }
    private System.Collections.IEnumerator LoadSceneAfterDelayCoroutine(int sceneNumber, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneNumber);
    }
}
