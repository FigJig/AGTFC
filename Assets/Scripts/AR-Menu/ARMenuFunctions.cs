using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARMenuFunctions : MonoBehaviour {

    public GameObject[] menus; //0 = Objectives Menu, 1 = Dictionary Menu

	public void ObjectivesClick()
    {
        menus[0].SetActive(true);
        menus[1].SetActive(false);
    }

    public void DictionaryClick()
    {
        menus[1].SetActive(true);
        menus[0].SetActive(false);
    }
}
