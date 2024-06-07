using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnnextrode : MonoBehaviour
{
    [SerializeField] GameObject road;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(road,new Vector3(-1.944852f, 4.759911f, 316f),Quaternion.identity);
        }
    }

}
