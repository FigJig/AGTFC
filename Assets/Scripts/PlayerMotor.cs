using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;
    private Rigidbody rb;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float curCameraRotationX = 0f;
    [SerializeField]
    private float cameraRotationLimit = 85f;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        PerformMovement();
        PerformRotation();
    }

    public void Move(Vector3 _velocity)
    {
       velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void CameraRotate (float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    void PerformMovement()
    {
        if(velocity != Vector3.zero)
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if(cam != null)
        {
            curCameraRotationX -= cameraRotationX;
            curCameraRotationX = Mathf.Clamp(curCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
            cam.transform.localEulerAngles = new Vector3(curCameraRotationX, 0f, 0f);
        }
    }
}
