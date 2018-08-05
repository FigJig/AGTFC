using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour {

    public Renderer rend { get; private set; }

    void Start()
    {
        rend = GetComponent<Renderer>();
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
