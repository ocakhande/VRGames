using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform bombSpawnPoint;
    public GameObject explosionPrefab;
    public bool isbombActive = false;
    public static BombManager Instance;

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
        if (StarScore.Instance.starScore > 0 && StarScore.Instance.starScore % 5 == 0)
        {
            Instantiate(bombPrefab, bombSpawnPoint);
            //starScore = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            var explosion = Instantiate(explosionPrefab, other.gameObject.transform.position, other.gameObject.transform.rotation);
            ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
            var main = ps.main;
            this.GetComponent<AudioSource>().Play();
            isbombActive = true;
            

        }
    }
}
