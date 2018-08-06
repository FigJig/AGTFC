using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour {

    public GameObject NPC { get; private set; }
    public GameObject Object { get; private set; }

    [SerializeField]
    private GameObject arMenu;
    [SerializeField]
    private Transform arMenuGrid;
    [SerializeField]
    private GameObject arMenuDictRowPrefab;
    [SerializeField]
    private LayerMask interactableLayer;
    private LanguageDictionary languageDictionary;

    void Start()
    {
        NPC = null;
        Object = null;
        languageDictionary = new LanguageDictionary();
        languageDictionary.Initialize();
    }
	
	void Update () {

        ARMenuFunctions();

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f, interactableLayer))
        {
            if (Input.GetButtonDown("Fire1") && !arMenu.activeSelf)
            {
                //Gets the NPC the player clicks on to help translate for them
                if (hit.transform.gameObject.tag == "NPC")
                {
                    if (NPC != null)
                    {
                        GameObject _prevNPC = NPC;

                        NPC.gameObject.GetComponent<NPC>().RemoveOutline();

                        NPC = hit.transform.gameObject;

                        if (_prevNPC == NPC)
                        {
                            NPC.gameObject.GetComponent<NPC>().RemoveOutline();
                            NPC = null;
                            return;
                        }
                    }
        
                    NPC = hit.transform.gameObject;
                    NPC.gameObject.GetComponent<NPC>().Interaction();
                }

                //If it's an object they've clicked, this is the object they want translated
                if (hit.transform.gameObject.tag == "Object")
                {
                    if (Object != null)
                    {
                        GameObject _prevObject = Object;

                        Object.gameObject.GetComponent<InteractableObject>().RemoveOutline();

                        Object = hit.transform.gameObject;

                        if(_prevObject == Object)
                        {
                            Object.gameObject.GetComponent<InteractableObject>().RemoveOutline();
                            Object = null;
                            return;
                        }
                    }

                    Object = hit.transform.gameObject;
                    Object.gameObject.GetComponent<InteractableObject>().AddOutline();
                }               
            }
        }

        //When an NPC and Object has been clicked on, the NPC will translate if they're a helpful NPC
        if (Input.GetKeyDown(KeyCode.Space) && NPC != null && Object != null)
        {
            if (NPC.gameObject.GetComponent<NPCHelpful>())
            {
                NPC.gameObject.GetComponent<NPCHelpful>().ObjectToTranslate = Object;

                string _objectEnglishWord;
                if (languageDictionary.AlienEnglishDictionary.TryGetValue(Object.gameObject.GetComponent<InteractableObject>().ObjectName, out _objectEnglishWord))
                    AddToGuide(Object.gameObject.GetComponent<InteractableObject>().ObjectName, _objectEnglishWord);
                else
                    throw new Exception("Alien object name is wrong; not found in the LanguageDictionary."); //Used to catch any errors with inputting wrong names in the dictionary or ObjectName
            }

            NPC.gameObject.GetComponent<NPC>().Translate();
            Object.gameObject.GetComponent<InteractableObject>().Translated();
            NPC = null;
            Object = null;
        }
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
            switch(arMenu.activeSelf)
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

    //Adds new translated word to AR guide dictionary
    void AddToGuide(string _alienWord, string _englishWord)
    {
        GameObject prefab = Instantiate(arMenuDictRowPrefab, arMenuGrid.transform) as GameObject;
        prefab.transform.SetAsLastSibling();
        prefab.transform.GetChild(0).gameObject.GetComponent<Text>().text = _alienWord;
        prefab.transform.GetChild(1).gameObject.GetComponent<Text>().text = _englishWord;
    }
}
