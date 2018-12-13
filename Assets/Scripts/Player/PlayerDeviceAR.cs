using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeviceAR : MonoBehaviour {

    [SerializeField]
    private GameObject arMenu;

	void Update () {
        ARMenuFunctions();
    }

    void ARMenuFunctions()
    {
        if (!arMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch (arMenu.activeSelf)
            {
                case true:
                    arMenu.SetActive(false);
                    break;
                case false:
                    arMenu.SetActive(true);
                    break;
            }
        }

    }
}
