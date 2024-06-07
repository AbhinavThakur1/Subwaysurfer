using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinIncrease : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<ObjectDestroy>().Playcoinpicksound();
            FindFirstObjectByType<ObjectDestroy>().coins += 1;
            Destroy(gameObject);
        }
    }
}
