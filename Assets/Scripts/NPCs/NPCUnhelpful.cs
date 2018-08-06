using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUnhelpful : NPC
{

    public override void Interaction()
    {
        Debug.Log("I'm an unhelpful NPC");
        AddOutline();
    }

    public override void Translate()
    {
        dialogue.text = "!!!!!!!!";
        RemoveOutline();
    }
}
