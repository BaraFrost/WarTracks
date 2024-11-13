using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class DokController : MonoBehaviour
{
    [SerializeField]
    private Transform gun;
    [SerializeField]
    private float i;
    public float maxDistance = 10f;
    [SerializeField]
    private Transform dokObj;
    public LayerMask enemy;
    [SerializeField]
    private float distancew;
    public float k;
    [SerializeField]
    private Vector2 startDokObj;
    void Start()
    {
        startDokObj = dokObj.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = transform.right;
        RaycastHit2D hit = Physics2D.Raycast(gun.position,direction, maxDistance, enemy);
        if (hit.collider != null)
        {
            Vector2 intersectionPoint = hit.point;
            dokObj.position= intersectionPoint;
           
            
            // Если луч попал в объект, выводим расстояние
            distancew = hit.distance;
        }
        else 
        {
            dokObj.position = (Vector2)transform.position + direction * maxDistance;
        }
        Debug.DrawRay(transform.position, direction * maxDistance, Color.red);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

       
            if (collision.gameObject.TryGetComponent<EnemyController>(out var enemyDist))
            {
            //dok.position
                i= enemyDist.distance;

            }
       
    }
}
