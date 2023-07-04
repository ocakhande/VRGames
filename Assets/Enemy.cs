using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform playerTransform;
    private Transform roadPoint;
    private int BulletCount = 0;
    public EnemyManager manager;
    private CapsuleCollider capsulecollider;
    public bool isDead = false;
    public bool isSpawned = false;
    public float enemyHealth;
    public float healthBar = 1f;
    public float currentHealth;
    public Gun gun;
    [SerializeField] private Slider healthSlider;



    private void Start()
    {
        manager = FindObjectOfType<EnemyManager>();
        capsulecollider = gameObject.GetComponent<CapsuleCollider>();

        if (gameObject.CompareTag("EnemyA"))
        {
            enemyHealth = 50;
        }
        else if (gameObject.CompareTag("EnemyB"))
        {
            enemyHealth = 100;
        }
        else if (gameObject.CompareTag("EnemyC"))
        {
            enemyHealth = 150;
        }
        else
        {
            enemyHealth = 200;
        }

    }


    private void Awake()
    {

        playerTransform = GameObject.Find("Camera Offset").transform;
        roadPoint = GameObject.Find("Spawn").transform;
        gameObject.transform.DOMove(roadPoint.position, 1f);

    }

    private void Update()
    {
        healthSlider.value = currentHealth / healthBar;

        if (BombManager.Instance.isbombActive == true)
        {
            currentHealth = 0;
            BombManager.Instance.isbombActive = false;  
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spawn"))
        {
            Debug.Log("carpti");
            isSpawned = true;
            gameObject.GetComponent<Animator>().SetBool("Falling", true);


            var enemyTarget = playerTransform.position - Vector3.up * 2.25f;
            gameObject.transform.DOMove(enemyTarget, 5f)
                .SetEase(Ease.Linear)
                .SetSpeedBased();
            capsulecollider.transform.rotation = Quaternion.identity;

            gameObject.transform.DOLookAt(playerTransform.position, 0.2f);



        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            StarScore.Instance.starScore++;
            currentHealth = (enemyHealth - gun.Damage)/enemyHealth;
            Debug.Log("kursun carpti");
            BulletCount++;
            
            if (currentHealth <= 0)
            {
                gameObject.transform.DOLookAt(playerTransform.position, 1f)
                    .OnComplete(() =>
                    {
                        gameObject.GetComponent<Animator>().SetBool("Dead", true);
                        isDead = true;
                        StartCoroutine(DestroyEnemyAfterAnimation());

                    });
                isDead = false;
            }
        }
    }
    private IEnumerator DestroyEnemyAfterAnimation()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
