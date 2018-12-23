using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public string ObjectName;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void Translated()
    {
        RemoveOutline();
    }
    public void AddOutline()
    {
        rend.material.SetFloat("_ASEOutlineWidth", 0.06f);
    }

    public void RemoveOutline()
    {
        rend.material.SetFloat("_ASEOutlineWidth", 0.0f);
    }

}
