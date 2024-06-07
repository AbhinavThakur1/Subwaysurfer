using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movetheroad : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0f, 0f, FindFirstObjectByType<ObjectDestroy>().speed) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
