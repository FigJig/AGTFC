using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class NPC : MonoBehaviour {

    public Renderer rend { get; private set; }
    [HideInInspector]
    public TextMeshPro dialogue;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        dialogue = transform.GetChild(1).GetComponent<TextMeshPro>();
    }

    public void AddOutline()
    {
        rend.material.SetFloat("_ASEOutlineWidth", 0.06f);
    }

    public void RemoveOutline()
    {
        rend.material.SetFloat("_ASEOutlineWidth", 0.0f);
    }

    public abstract void Interaction();

    public abstract void Translate();
}
