using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhitaobjectchecker : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Time.timeScale = 0f;
        }
    }
}
