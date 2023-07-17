using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BulletManager : MonoBehaviour
{
    public GameObject bullet;
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
            GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position,spawnPoint.rotation);
            //spawnedBullet.transform.position = spawnPoint.position;
            this.GetComponent<AudioSource>().Play();
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * gun.fireSpeed;
            Destroy(spawnedBullet, 1);
        }
    }


}
