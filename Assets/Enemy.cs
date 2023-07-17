
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;
using UnityEditor;
//using System.Diagnostics;

public class Enemy : MonoBehaviour
{
    private Transform playerTransform;
    private Transform roadPoint;
    public EnemyManager manager;
    private CapsuleCollider capsulecollider;
    public bool isDead = false;
    public bool isSpawned = false;
    public float healthBar = 1f;
    public float currentHealth;
    public GameObject explosionPrefab;
    public bool isbombActive = false;
    [SerializeField] private Slider healthSlider;
    private Rigidbody rb;
    NavMeshAgent enemyNav;

    public EnemyData enemyData;
    public int enemyHealth1;
    public int enemySpeed1;
    public int enemyDamage1;
    public int enemyPower1;

    public GunManager gunManager;

    private void Start()
    {
        GunManager gunManager = FindObjectOfType<GunManager>();

        Debug.Log("enemyHealth" + enemyData.Health);
        rb = GetComponent<Rigidbody>();

        manager = FindObjectOfType<EnemyManager>();

        capsulecollider = gameObject.GetComponent<CapsuleCollider>();

        currentHealth = healthBar;

        enemyNav = GetComponent<NavMeshAgent>();
        enemyNav.enabled = false;
    }

    private void Awake()
    {

        playerTransform = GameObject.Find("Camera Offset").transform;
        roadPoint = GameObject.Find("Spawn").transform;

    }

    private void Update()
    {

        //healthSlider.value = currentHealth/enemyData.Health;
        if (isSpawned && !isDead)
        {
            if (!enemyNav.enabled)
            {
                enemyNav.enabled = true;
                enemyNav.SetDestination(playerTransform.position);
            }
            else
            {
                enemyNav.SetDestination(playerTransform.position);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("spwn"))
        {
            gameObject.transform.DOMove(roadPoint.position, 1f)
      .SetEase(Ease.Linear)
      .OnComplete(() =>
      {
          isSpawned = true;
          Debug.Log("düstü0");
          gameObject.GetComponent<Animator>().SetBool("Falling", true);
      });
        }


        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("currentHealth" + currentHealth);
            SetEnemyHealth();
            if (currentHealth <= 0)
            {
                gameObject.transform.DOLookAt(playerTransform.position, 1f)
                    .OnComplete(() =>
                    {
                        rb.isKinematic = false;
                        gameObject.GetComponent<Animator>().SetBool("Dead", true);
                        enemyNav.isStopped = true;
                        StartCoroutine(DeactivateEnemyAfterAnimation());

                    });
            }
        }



        if (other.gameObject.CompareTag("Bomb"))
        {
            var explosion = Instantiate(explosionPrefab, other.gameObject.transform.position, other.gameObject.transform.rotation);
            ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
            var main = ps.main;

            this.GetComponent<AudioSource>().Play();

            currentHealth = 0;
            rb.isKinematic = false;
            gameObject.GetComponent<Animator>().SetBool("Dead", true);
            enemyNav.isStopped = true;

           StartCoroutine(DeactivateEnemyAfterAnimation());
            other.gameObject.SetActive(false);
            

        }

    }

    public void SetEnemyHealth()
    {
        currentHealth = currentHealth - ((enemyData.Health - GunManager.Instance.gunDamage) / enemyData.Health);
        healthSlider.value = currentHealth;
    }

    public void SetEnemyData()
    {

        gameObject.GetComponent<Animator>().SetBool("Dead", false);
        currentHealth = healthBar;
        isSpawned = false;
        enemyDamage1 = enemyData.enemyDamage;
        enemyHealth1 = enemyData.Health;
        enemyPower1 = enemyData.enemyPower;
        enemySpeed1 = enemyData.enemySpeed;
        enemyNav.enabled = false;
    }


    private IEnumerator DeactivateEnemyAfterAnimation()
    {
        yield return new WaitForSeconds(2f);

        if (gameObject.activeSelf)
        {
            isDead = true;
            Animator animator = gameObject.GetComponent<Animator>();
            animator.enabled = false;
            EnemyManager.Instance.enemyCount = 0;
            EnemyManager.Instance.deadCount++;
        }
        else
        {
            yield return null;
            StartCoroutine(DeactivateEnemyAfterAnimation());
        }


    }
}


