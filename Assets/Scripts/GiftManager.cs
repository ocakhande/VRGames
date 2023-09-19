using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftManager : MonoBehaviour
{
    public GameObject giftPrefab;
    public int spawnCount = 3;
    public List<Transform> spawnPoints = new List<Transform>();
    private bool shouldSpawnGifts = false;

     private void Update()
    {
        if (EnemyManager.Instance.deadCount % 5 == 0 && !shouldSpawnGifts && EnemyManager.Instance.deadCount>0)
        {
            shouldSpawnGifts = true;
            StartCoroutine(SpawnGifts());
        }
    }

    private IEnumerator SpawnGifts()
    {
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        int giftsToSpawn = Mathf.Min(spawnCount, availableSpawnPoints.Count);

        for (int i = 0; i < giftsToSpawn; i++)
        {
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[randomIndex];

            GameObject giftObject = ObjectPooling.instance.GetPoolObject(1);
            giftObject.transform.position = spawnPoint.position;

            availableSpawnPoints.RemoveAt(randomIndex);
            yield return null; 
        }
    }

}


//public GameObject giftPrefab;
//public Transform giftSpawn1;
//public Transform giftSpawn2;
//public Transform giftSpawn3;
//private int spawnedGiftCount = 0;
//private int maxGiftCount = 1;
//private float giftDuration = 7f;
//private List<GameObject> activeGifts = new List<GameObject>();

//void Update()
//{
//    if (EnemyManager.Instance.deadCount % 5 == 0 && spawnedGiftCount < maxGiftCount)
//    {
//        GameObject gift = ObjectPooling.instance.GetPoolObject(1);
//        if (gift != null)
//        {
//            Transform spawnTransform = GetRandomSpawnTransform();
//            if (spawnTransform != null)
//            {
//                gift.transform.position = spawnTransform.position;
//                gift.transform.rotation = spawnTransform.rotation;
//                gift.SetActive(true);
//                activeGifts.Add(gift);
//                spawnedGiftCount++;

//                StartCoroutine(DisableGift(gift, giftDuration));
//            }
//            else
//            {
//                ObjectPooling.instance.SetPoolObject(gift, 1);
//            }
//        }
//    }
//}

//private Transform GetRandomSpawnTransform()
//{
//    List<Transform> SpawnPoints = new List<Transform>();

//    if (giftSpawn1.childCount == 0)
//        SpawnPoints.Add(giftSpawn1);

//    if (giftSpawn2.childCount == 0)
//        SpawnPoints.Add(giftSpawn2);

//    if (giftSpawn3.childCount == 0)
//        SpawnPoints.Add(giftSpawn3);

//    if (SpawnPoints.Count == 0)
//        return null;

//    int randomIndex = Random.Range(0, SpawnPoints.Count);
//    return SpawnPoints[randomIndex];
//}

//private IEnumerator DisableGift(GameObject gift, float delay)
//{
//    yield return new WaitForSeconds(delay);
//    if (activeGifts.Contains(gift))
//    {
//        activeGifts.Remove(gift);
//        ObjectPooling.instance.SetPoolObject(gift, 1);
//        spawnedGiftCount--;
//    }
//}

//private void OnDisable()
//{
//    foreach (var gift in activeGifts)
//    {
//        ObjectPooling.instance.SetPoolObject(gift, 1);
//    }
//    activeGifts.Clear();
//    spawnedGiftCount = 0;
//}

//private void OnTriggerEnter(Collider other)
//{
//    if (other.gameObject.CompareTag("Bullet"))
//    {

//        EnemyManager.Instance.deadCount += 5;
//        StarScore.instance.UpdateScore();
//    }
//}
