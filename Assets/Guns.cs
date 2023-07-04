using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public float Damage;

    
    void Start()
    {
        if (gameObject.CompareTag("Gun1"))
        {
            Damage = 10;
        }
        else if (gameObject.CompareTag("Gun2"))
        {
            Damage = 12.5f;
        }
        else if (gameObject.CompareTag("Gun3"))
        {
            Damage = 25f;
        }
        else
        {
            Damage = 50f;
        }

    }
     
    void Update()
    {
        
    }
}
