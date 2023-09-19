using UnityEngine;

public class BombManager : MonoBehaviour
{
    public GameObject BombPrefab;
    private Transform playerTransform;
    private int bombCost=1;

    private void Start()
    {
        playerTransform = GameObject.Find("CameraRig").transform;
    }

    public void CreateBomb()
    {
        if (StarScore.instance.cash >= bombCost)
        {
            BombPrefab = ObjectPooling.instance.GetPoolObject(2);
            Debug.Log("bomba uretiliyor");
            BombPrefab.transform.position = playerTransform.position;
            Debug.Log("bu bloga girdi");
            StarScore.instance.cash-=bombCost;
            StarScore.instance.UpdateScore();

        }

    }
}
