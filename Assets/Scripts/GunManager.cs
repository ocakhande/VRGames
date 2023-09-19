using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GunManager : MonoBehaviour
{
    public static GunManager Instance;

    public List<Gun> gunList = new List<Gun>();
    public Transform gunPos;

    public Gun scriptableGun;
    public float gunDamage;
    public float gunFireSpeed;
    public int gunCost;
    public int gunID;
    private Transform playerTransform;


    public int selectedGunID;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        playerTransform = GameObject.Find("CameraRig").transform;
    }


    public void SetSelectedGunID(int id)
    {
        if (id >= 0 && id < gunList.Count)
        {
            selectedGunID = id;
            Debug.Log("Selected Gun ID: " + id);
            CreateGun();
        }
    }
    public void SetGunData()
    {
        if (selectedGunID >= 0 && selectedGunID < gunList.Count)
        {
            Gun selectedGun = gunList[selectedGunID];
            if (selectedGun != null)
            {
                scriptableGun = selectedGun;
                gunDamage = selectedGun.Damage;
                gunFireSpeed = selectedGun.fireSpeed;
                gunCost = selectedGun.Cost;
                gunID = selectedGun.Id;
            }
        }
    }

    public void CreateGun()
    {
        Debug.Log("Creating gun");

        Gun selectedGun = gunList[selectedGunID];
        SetGunData();

        if (selectedGun != null && selectedGun.Cost <= StarScore.instance.cash)
        {
            GameObject instantiatedGun = Instantiate(selectedGun.gun);
            StarScore.instance.cash -= selectedGun.Cost;
            StarScore.instance.UpdateScore();

            instantiatedGun.transform.position = playerTransform.position;

    }
    }

    
}



//public List<Gun> guns = new List<Gun>();
////public int currentGun = 0;
////public Canvas canvas;

//private void Awake()
//{
//    if (Instance == null)
//    {
//        Instance = this;
//    }
//    else
//    {
//        Destroy(gameObject);
//    }
//    AccessButtonID();
//}

//private void Start()
//{
//    //ActivateGun(currentGun);
//}

//void Update()
//{
//    //if (EnemyManager.Instance.DeadCount == 5 && currentGun <= gunList.Count - 2)
//    //{
//    //    currentGun++;
//    //    DeactivateGun(currentGun - 1);
//    //    ActivateGun(currentGun);
//    //    EnemyManager.Instance.DeadCount = 0;

//    //    canvas.enabled = true;
//    //    StartCoroutine(DisableCanvasAfterDelay(2f));
//    //}
//}
//public void SelectGun()
//{
//    Gun gun = Instantiate(guns[0]);
//}

////private void ActivateGun(int index)
////{
////    if (index >= 0 && index < gunList.Count)
////    {
////        GameObject gun = gunList[index];
////        Gun gunScript = guns[index];

////        Gun firstGun = guns[0];
////        gunScript.Damage = firstGun.Damage;
////        gunScript.moveSpeed = firstGun.moveSpeed;
////        gunScript.fireSpeed = firstGun.fireSpeed;

////        gun.SetActive(true);
////    }
////}

////private void DeactivateGun(int index)
////{
////    if (index >= 0 && index < gunList.Count)
////    {
////        GameObject gun = gunList[index];
////        gun.SetActive(false);
////    }
////}

////private IEnumerator DisableCanvasAfterDelay(float delay)
////{
////    yield return new WaitForSeconds(delay);
////    canvas.enabled = false;
////}

////public Gun GetGun(int index)
////{
////    if (index >= 0 && index < guns.Count)
////    {
////        return guns[index];
////    }
////    else
////    {
////        return null;
////    }
////}
