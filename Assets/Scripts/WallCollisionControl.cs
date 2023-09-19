using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisionControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("kursun duvara carpti");
            ObjectPooling.instance.SetPoolObject(other.gameObject,0);
        }
    }
}
