using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class UIController : MonoBehaviour
{
    private Transform playerTransform;
    public GameObject menuCanvas;
    private bool isActive = false;
    private bool isButtonPressed = false;
    public float menuDistance = 7.0f; 

    private void Awake()
    {
        playerTransform = GameObject.Find("LeftController").transform;
    }

    void Update()
    {
        if (InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue))
        {
            if (primaryButtonValue && !isButtonPressed)
            {
                isButtonPressed = true;
                ToggleMenu();
            }
            else if (!primaryButtonValue && isButtonPressed)
            {
                isButtonPressed = false;
            }
        }
    }

    void ToggleMenu()
    {
        if (isActive)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    void OpenMenu()
    {

        Vector3 menuPosition = playerTransform.position + playerTransform.forward * menuDistance;
        menuCanvas.transform.position = menuPosition;
        menuCanvas.transform.rotation = Quaternion.LookRotation(playerTransform.forward);
        menuCanvas.transform.Rotate(0f, 90f, 0f); 
        menuCanvas.SetActive(true);
        isActive = true;
    }

    public void CloseMenu()
    {
        menuCanvas.SetActive(false);
        isActive = false;
    }
}