using UnityEngine;

public class BombManager : MonoBehaviour
{
    public GameObject Bomb;

    private void Update()
    {
       
            if (EnemyManager.Instance.DeadCount == 5)
            {
                Debug.Log("bomb2");
                Bomb.SetActive(true);
            }

    }
}
