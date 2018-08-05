using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    [SerializeField]
    private LayerMask interactableLayer;

    public GameObject NPC { get; private set; }
    public GameObject Object { get; private set; }

    void Start()
    {
        NPC = null;
        Object = null;
    }
	
	void Update () {

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f, interactableLayer))
        {
            if (Input.GetButtonDown("Fire1"))
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
                NPC.gameObject.GetComponent<NPCHelpful>().ObjectToTranslate = Object;

            NPC.gameObject.GetComponent<NPC>().Translate();
            Object.gameObject.GetComponent<InteractableObject>().Translated();
            NPC = null;
            Object = null;
        }
    }
}
