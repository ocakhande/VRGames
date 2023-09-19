using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public List<Enemy> enemies = new List<Enemy>();
    public Transform spawnPoint;
    public Enemy currentEnemy;
    public int enemyCount = 0;
    public int deadCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ActivateRandomEnemy();
    }

    private void Update()
    {
        if (currentEnemy != null && currentEnemy.isDead)
        {

            deadCount += 150;
            StarScore.instance.cash += 150;
            StarScore.instance.UpdateScore();
            DeActivatedEnemy();
            currentEnemy = null;

            StartCoroutine(waitForActive());
        }
    }

    public void ActivateRandomEnemy()
    {
        if (enemies.Count == 0)
        {
            return;
        }

        int enemyIndex = Random.Range(0, enemies.Count);
        currentEnemy = enemies[enemyIndex];
        currentEnemy.isDead = false;

        //currentEnemy.SetEnemyData();
        currentEnemy.gameObject.SetActive(true);
        currentEnemy.transform.position = spawnPoint.transform.position;
        currentEnemy.gameObject.GetComponent<Animator>().enabled = true;
        currentEnemy.enabled = true;


        enemyCount++;
    }

    public void DeActivatedEnemy()
    {

        currentEnemy.SetEnemyData();
        // currentEnemy.gameObject.GetComponent<Animator>().enabled = false;
        currentEnemy.gameObject.SetActive(false);

    }


    public IEnumerator waitForActive()
    {
        yield return new WaitForSeconds(1f);
        ActivateRandomEnemy();

    }

}

// public static EnemyManager Instance;
// public List<Enemy> enemy = new List<Enemy>();
// public Transform SpawnPoint;
// [SerializeField] Enemy spawnedEnemy;
// private int enemyIndex;
// public int enemyCount = 0;
// public int DeadCount = 0;

//[SerializeField] EnemyData enemyData;
// public int enemyHealth;
// public int enemySpeed;
// public int enemyDamage;
// private void Awake()
// {
//     if (Instance == null)
//     {
//         Instance = this;
//     }
//     else
//     {
//         Destroy(gameObject);
//     }
// }

// private void Update()
// {
//     if(enemyCount==0)
//     {
//         SpawnEnemy();


//     }
//     if (spawnedEnemy.isDead == true)
//     {
//         DeadCount++;
//         StarScore.instance.cash++;
//         StarScore.instance.UpdateScore();
//         enemyCount = 0;
//     }

//     if(DeadCount>0 && DeadCount%5==0)
//     {
//         spawnedEnemy.enemyData.enemySpeed++;
//         spawnedEnemy.enemyData.Health += 10;
//     }
// }

// public void SpawnEnemy()
// {
//     enemyIndex = Random.Range(0, enemy.Count);
//     Debug.Log(enemyIndex.ToString());

//     spawnedEnemy = enemy[enemyIndex];
//     spawnedEnemy.transform.position = SpawnPoint.position;
//     spawnedEnemy = Instantiate(enemy[enemyIndex], SpawnPoint.position, Quaternion.identity);
//     enemyCount++;

//     SetEnemyData();


// }
// public void SetEnemyData()
// {
//     enemyHealth = spawnedEnemy.enemyData.Health ;
//     enemySpeed = spawnedEnemy.enemyData.enemySpeed;
//     enemyDamage = spawnedEnemy.enemyData.enemyDamage;
// }



