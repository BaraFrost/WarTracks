using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private GameObject enemy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            Instantiate(enemy, spawnPoint.transform.position, transform.rotation);
            
        }
    }
    public void SpawnEnemy()
    {
        Instantiate(enemy, spawnPoint.transform.position, transform.rotation);
    }
}
