using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class UIController : MonoBehaviour
{
    public GameObject menuCanvas;
    private bool isActive = false;
    private bool isButtonPressed = false;

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
        menuCanvas.SetActive(true);
        isActive = true;
    }

    public void CloseMenu()
    {
        menuCanvas.SetActive(false);
        isActive = false;
    }
}