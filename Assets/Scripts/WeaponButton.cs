using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public int buttonID; // Silah Scriptable Object verisi
    //public GunManager gunManager;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClickEvent);
    }

    public void ButtonClickEvent()
    {
        Debug.Log("Clicked button ID: " + buttonID);
        GunManager.Instance.SetSelectedGunID(buttonID);
        //GunManager.Instance.CreateGun();
    }
}