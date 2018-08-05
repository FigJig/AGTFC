using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUnhelpful : NPC
{

    public override void Interaction()
    {
        Debug.Log("I'm an unhelpful NPC");
        rend.material.SetFloat("_ASEOutlineWidth", 0.06f);
    }

    public override void Translate()
    {
        Debug.Log("Get out of here foreigner!");
        rend.material.SetFloat("_ASEOutlineWidth", 0.0f);
    }
}
