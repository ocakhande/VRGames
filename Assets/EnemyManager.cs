using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public List<Enemy> enemy = new List<Enemy>();
    public Transform SpawnPoint;
    [SerializeField] Enemy spawnedEnemy;
    private int enemyIndeks;
    public int enemyCount = 0;
    public int DeadCount = 0;

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

    private void Update()
    {
        if(enemyCount==0)
        {
            SpawnEnemy();
        }
        if (spawnedEnemy.isDead == true)
        {
            DeadCount++;
            enemyCount = 0;
        }
    }

    public void SpawnEnemy()
    {
        enemyIndeks = Random.Range(0, enemy.Count);
        Debug.Log(enemyIndeks.ToString());
        spawnedEnemy = Instantiate(enemy[enemyIndeks], SpawnPoint.position, Quaternion.identity);
        enemyCount++;

    }

}
