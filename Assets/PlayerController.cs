using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    public float playerHealth = 50;
    public float currentHealth;
    public float healthBar = 1f;
    public Slider healthSlider;
    public Gun gun;
    public int CollisionCount = 0;
    public ActionBasedContinuousMoveProvider moveProvider;

    void Start()
    {
        GunManager gunManager = FindObjectOfType<GunManager>();
        if (gunManager != null)
        {
            //gun = gunManager.GetGun(GunManager.Instance.currentGun);

        }

        currentHealth = playerHealth;


        //moveProvider.moveSpeed = gun.PlayerSpeed;  
    }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Düsman Degdi");
                CollisionCount++;
            }
            //if (EnemyManager.Instance.enemyHealth <= 100 && CollisionCount == 2)
            //{
            //    currentHealth -= (EnemyManager.Instance.enemyDamage);
            //    CollisionCount = 0;
            //}
            //else if (EnemyManager.Instance.enemyHealth >= 100 && CollisionCount == 1)
            //{
            //    currentHealth -= (EnemyManager.Instance.enemyDamage);
            //    CollisionCount = 0;
            //}
            //if (currentHealth <= 0)
            //{
            //    GameOver.instance.ShowGameOver();
            //    //Time.timeScale = 0;
            //}

            healthSlider.value = currentHealth / playerHealth;

        }
    }


