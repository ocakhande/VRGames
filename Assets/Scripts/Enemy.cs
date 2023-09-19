using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    private Transform playerTransform;
    private Transform roadPoint;
    public EnemyManager manager;
    private CapsuleCollider capsulecollider;
    public bool isDead = false;
    public bool isSpawned = false;
    public float maxHealth;
    public float currentHealth;
    public GameObject explosionPrefab;
    public bool isbombActive = false;
    public Image healthbarSprite;
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

        maxHealth = enemyData.Health;
        currentHealth = maxHealth;
        healthbarSprite.fillAmount = currentHealth / maxHealth;

        enemyNav = GetComponent<NavMeshAgent>();
        enemyNav.enabled = false;
    }

    private void Awake()
    {

        playerTransform = GameObject.Find("CameraRig").transform;
        roadPoint = GameObject.Find("Spawn").transform;

    }


    private void Update()
    {
        if (isSpawned && !isDead)
        {
            if (!enemyNav.enabled)
            {
                enemyNav.enabled = true;
                enemyNav.SetDestination(playerTransform.position);
            }
            else
            {
                // Düþmanýn kendi hýzýný kullan
                enemyNav.speed = enemyData.enemySpeed;

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
            ObjectPooling.instance.SetPoolObject(other.gameObject, 0);
            currentHealth -= GunManager.Instance.gunDamage;
            Debug.Log("currentHealth" + currentHealth);

            if (currentHealth == 0)
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
            else
                healthbarSprite.fillAmount = currentHealth / maxHealth;
        }



        if (other.gameObject.CompareTag("Bomb"))
        {
            var explosion = Instantiate(explosionPrefab, other.gameObject.transform.position, other.gameObject.transform.rotation);
            ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
            var main = ps.main;

            this.GetComponent<AudioSource>().Play();
            ObjectPooling.instance.SetPoolObject(other.gameObject, 2);
            currentHealth = 0;
            rb.isKinematic = false;
            gameObject.GetComponent<Animator>().SetBool("Dead", true);
            enemyNav.isStopped = true;

            StartCoroutine(DeactivateEnemyAfterAnimation());
            other.gameObject.SetActive(false);


        }

    }

    public void SetEnemyData()
    {

        gameObject.GetComponent<Animator>().SetBool("Dead", false);
        currentHealth = maxHealth;
        isSpawned = false;
        healthbarSprite.fillAmount = 1;
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
