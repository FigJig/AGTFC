using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHelpful : NPC {

    public string textToDisplay;

    public override void Interaction()
    {
        Debug.Log("I'm a helpful NPC");
        AddOutline();
    }

    public override void Translate()
    {
        dialogue.text = textToDisplay;
        RemoveOutline();
    } 
}
