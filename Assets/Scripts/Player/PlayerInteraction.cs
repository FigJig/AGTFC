using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour {

    public NPC curNPC { get; private set; }
    public InteractableObject interactableObject { get; private set; }

    public GrabbableObject curGrabbedObj;

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
        languageDictionary = new LanguageDictionary();
        languageDictionary.Initialize();
    }
	
	void Update () {

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f, interactableLayer))
        {
            if (Input.GetButtonDown("Fire1") && !arMenu.activeSelf)
            {
                //Gets the NPC the player clicks on to help translate for them
                if (hit.transform.gameObject.tag == "NPC")
                {
                    if (curNPC != null)
                    {
                        NPC _prevNPC = curNPC;

                        curNPC.RemoveOutline();

                        curNPC = hit.transform.GetComponent<NPC>();

                        if (_prevNPC == curNPC)
                        {
                            curNPC.RemoveOutline();
                            curNPC = null;
                            return;
                        }
                    }

                    curNPC = hit.transform.GetComponent<NPC>();
                    curNPC.Interaction();
                }

                //If it's an object they've clicked, this is the object they want translated
                if (hit.transform.gameObject.tag == "Object")
                {
                    if (interactableObject != null)
                    {
                        InteractableObject _prevObject = interactableObject;

                        interactableObject.RemoveOutline();

                        interactableObject = hit.transform.GetComponent<InteractableObject>();

                        if(_prevObject == interactableObject)
                        {
                            interactableObject.RemoveOutline();
                            interactableObject = null;
                            return;
                        }
                    }

                    interactableObject = hit.transform.GetComponent<InteractableObject>();
                    interactableObject.AddOutline();
                }

                if (hit.transform.gameObject.tag == "Grabbable")
                {
                    if (curGrabbedObj != null)
                    {
                        curGrabbedObj = null;
                        return;
                    }

                    curGrabbedObj = hit.transform.GetComponent<GrabbableObject>();
                    curGrabbedObj.grabbed = true;
                }
            }
        }

        //If the player is holding an object, drop it
        if (Input.GetButtonDown("Fire2") && !arMenu.activeSelf)
        {
            if (curGrabbedObj != null)
            {
                curGrabbedObj.grabbed = false;
            }
        }

        //When an NPC and Object has been clicked on, the NPC will translate if they're a helpful NPC
        if (Input.GetKeyDown(KeyCode.E) && curNPC != null && interactableObject != null)
        {     
            if (curNPC.GetComponent<NPCHelpful>())
            {
                string _objectAlienWord;
                if (languageDictionary.AlienEnglishDictionary.TryGetValue(interactableObject.ObjectName, out _objectAlienWord))
                    AddToGuide(_objectAlienWord, interactableObject.ObjectName);
                else
                    throw new Exception("English object name is wrong; not found in the LanguageDictionary."); //Used to catch any errors with inputting wrong names in the dictionary or ObjectName

                curNPC.GetComponent<NPCHelpful>().textToDisplay = _objectAlienWord;
            }

            curNPC.Translate();
            interactableObject.Translated();
            curNPC = null;
            interactableObject = null;
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
