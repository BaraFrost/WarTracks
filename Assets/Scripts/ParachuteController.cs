using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteController : MonoBehaviour
{
    [SerializeField]
    private GameObject heel;
    [SerializeField]
    private Transform spawn;
    [SerializeField]
    private float heelCount;
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if ((collision.gameObject.tag == "Wall") || (collision.gameObject.tag == "Player"))
        {
            if (collision.gameObject.TryGetComponent<EntityHealth>(out var health))
            {
                health.value += heelCount;
            }
            else
            {
                Instantiate(heel, spawn.transform.position, spawn.transform.rotation);

            }
            gameObject.SetActive(false);
           // Destroy(gameObject);
        }

    }
}
