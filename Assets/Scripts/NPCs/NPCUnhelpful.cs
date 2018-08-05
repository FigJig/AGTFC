using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUnhelpful : NPC
{

    public override void Interaction()
    {
        Debug.Log("I'm an unhelpful NPC");
        HasBeenInteracted = true;
        AddOutline();
    }

    public override void Translate()
    {
        Debug.Log("Get out of here foreigner!");
        HasBeenInteracted = false;
        RemoveOutline();
    }
}
