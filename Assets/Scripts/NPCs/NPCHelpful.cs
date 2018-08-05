using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHelpful : NPC {

    public GameObject ObjectToTranslate;

    public override void Interaction()
    {
        Debug.Log("I'm a helpful NPC");
        AddOutline();
    }

    public override void Translate()
    {
        Debug.Log("That is a " + ObjectToTranslate.gameObject.GetComponent<InteractableObject>().ObjectName);
        RemoveOutline();
    } 
}
