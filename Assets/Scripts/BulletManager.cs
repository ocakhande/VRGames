using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BulletManager : MonoBehaviour
{

    public GameObject spawnedBullet;
    //public int poolsize;
    public Transform spawnPoint;
    public Gun gun;


    void Start()
    {
        GunManager gunManager = FindObjectOfType<GunManager>();
        gun = gunManager.gunList[gunManager.selectedGunID];


        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        if (gun != null)
        {
            spawnedBullet = ObjectPooling.instance.GetPoolObject(0);
            spawnedBullet.transform.position = spawnPoint.position;
            spawnedBullet.transform.rotation = spawnPoint.rotation;
            this.GetComponent<AudioSource>().Play();
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * gun.fireSpeed;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            ObjectPooling.instance.SetPoolObject(spawnedBullet, 0);
        }
    }

}