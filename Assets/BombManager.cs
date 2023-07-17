using UnityEngine;

public class BombManager : MonoBehaviour
{
    public GameObject BombPrefab;
    private Transform playerTransform;
    private int bombCost=1;

    private void Awake()
    {
    
        playerTransform = GameObject.Find("Camera Offset").transform;
    }
    public void CreateBomb()
    {
        if (StarScore.instance.cash >= bombCost)
        {
            Debug.Log("bomba uretiliyor");
            BombPrefab.transform.position = playerTransform.position;
            BombPrefab.SetActive(true);
            StarScore.instance.cash-=bombCost;
            StarScore.instance.UpdateScore();

        }

    }
}
