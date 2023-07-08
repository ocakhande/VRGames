using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//using System.Diagnostics;

public class Enemy : MonoBehaviour
{
    private Transform playerTransform;
    private Transform roadPoint;
    private Transform bombSpawnPoint;
    //private int BulletCount = 0;
    public EnemyManager manager;
    private CapsuleCollider capsulecollider;
    public bool isDead = false;
    public bool isSpawned = false;
    public float enemyHealth;
    public float healthBar = 1f;
    public float currentHealth;
    public GameObject explosionPrefab;
    public bool isbombActive = false;
    public Gun gun;
    float moveSpeed = 5f;
    [SerializeField] private Slider healthSlider;
    private Rigidbody rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        manager = FindObjectOfType<EnemyManager>();
        gun = FindObjectOfType<Gun>();

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
        currentHealth = healthBar;
    }


    private void Awake()
    {
        bombSpawnPoint = GameObject.Find("BombSpawnPoint").transform;
        playerTransform = GameObject.Find("Camera Offset").transform;
        roadPoint = GameObject.Find("Spawn").transform;
        gameObject.transform.DOMove(roadPoint.position, 1f);

    }

    private void Update()
    {
        //healthSlider.value = currentHealth / healthBar;
        healthSlider.value = currentHealth;


        if (isbombActive == true)
        {
            currentHealth = 0;
            isbombActive = false; 
            EnemyManager.Instance.enemyCount = 0;
            EnemyManager.Instance.DeadCount++;

            
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spawn"))
        {
            Debug.Log("carpti");
            isSpawned = true;
            gameObject.GetComponent<Animator>().SetBool("Falling", true);

            StartCoroutine(FollowPlayer());
        }

        IEnumerator FollowPlayer()
        {
            while (true)
            {
                Vector3 enemyTarget = playerTransform.position - Vector3.up * 1.25f;
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, enemyTarget, step);

                Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);

                yield return null;
            }
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            //StarScore.Instance.starScore++;
            currentHealth = currentHealth-(enemyHealth - gun.Damage)/enemyHealth;
            healthSlider.value = currentHealth;

            Debug.Log("kursun carpti");
            //BulletCount++;
            
            if (currentHealth <= 0)
            {
                gameObject.transform.DOLookAt(playerTransform.position, 1f)
                    .OnComplete(() =>
                    {
                        rb.isKinematic = false;
                        gameObject.GetComponent<Animator>().SetBool("Dead", true);
                        isDead = true;
                        StartCoroutine(DestroyEnemyAfterAnimation());

                    });
                isDead = false;
            }
        }

        if (other.gameObject.CompareTag("Bomb"))
        {
            Debug.Log("bomba carpti");
            var explosion = Instantiate(explosionPrefab, other.gameObject.transform.position, other.gameObject.transform.rotation);
            ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
            var main = ps.main;
            Debug.Log("patladi");

            this.GetComponent<AudioSource>().Play();
            isbombActive = true;
            Debug.Log("patladi2");


            other.gameObject.SetActive(false);
            other.transform.position = bombSpawnPoint.position;

            currentHealth = 0;
            rb.isKinematic = false;
            isDead = true;
            Debug.Log("Bomba pasif edildi.");
            gameObject.GetComponent<Animator>().SetBool("Dead", true);
            StartCoroutine(DestroyEnemyAfterAnimation());

        }
        isDead = false;

    }
    private IEnumerator DestroyEnemyAfterAnimation()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

