using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public List<GameObject> gunList = new List<GameObject>();
    private int currentGun = 0;
    public Canvas canvas;

    private void Start()
    {
        ActivateGun(currentGun);
    }

    void Update()
    {
        if (EnemyManager.Instance.DeadCount == 5 && currentGun <= gunList.Count - 2)
        {
            currentGun++;
            DeactivateGun(currentGun - 1);
            ActivateGun(currentGun);
            EnemyManager.Instance.DeadCount = 0;

            canvas.enabled = true;
            StartCoroutine(DisableCanvasAfterDelay(2f));
        }
    }

    private void ActivateGun(int index)
    {
        if (index >= 0 && index < gunList.Count)
        {
            GameObject gun = gunList[index];
            gun.SetActive(true);
        }
    }

    private void DeactivateGun(int index)
    {
        if (index >= 0 && index < gunList.Count)
        {
            GameObject gun = gunList[index];
            gun.SetActive(false);
        }
    }
    private IEnumerator DisableCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        canvas.enabled = false;
    }
    private IEnumerator WaitingDestroy()
    {
        yield return new WaitForSeconds(1.5f);
    }
}