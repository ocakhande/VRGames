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
    public float maxHealth;
    public float healthBar = 1f;
    public Gun gun;
    public Image _healthbarSprite;
    public int CollisionCount = 0;
    public ActionBasedContinuousMoveProvider moveProvider;

    void Start()
    {
        maxHealth = playerHealth;
        currentHealth = maxHealth;
        _healthbarSprite.fillAmount = currentHealth / maxHealth;

        GunManager gunManager = FindObjectOfType<GunManager>();
        if (gunManager != null)
        {
            //gun = gunManager.GetGun(GunManager.Instance.currentGun);

        }

        //moveProvider.moveSpeed = gun.PlayerSpeed;  
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            currentHealth -= (EnemyManager.Instance.currentEnemy.enemyPower1);

            if (currentHealth <= 0)
            {
                GameOver.instance.ShowGameOver();
                //Time.timeScale = 0;
            }
            else
            {
                _healthbarSprite.fillAmount = currentHealth / maxHealth;
            }
        }

    }
}


