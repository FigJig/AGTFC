using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour {

    public bool grabbed;
    private Camera mainCam;
    private Rigidbody rb;
    private MeshCollider collider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<MeshCollider>();
        mainCam = Camera.main;
        grabbed = false;
    }

    void Update () {
        if (grabbed)
        {
            rb.isKinematic = true;
            collider.isTrigger = true;
            transform.SetParent(mainCam.transform);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0f, -0.5f, 2f), 0.5f);
            transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        else
        {
            rb.isKinematic = false;
            collider.isTrigger = false;
            transform.parent = null;
        }
	}
}
