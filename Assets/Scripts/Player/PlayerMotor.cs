using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    public GameObject ARMenu;

    [SerializeField]
    private Camera cam;
    private Rigidbody rb;
    private PlayerController playerController;

    private Vector3 velocity = Vector3.zero;
    private Vector3 sprintVelocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float jumpHeight = 0f;
    private float cameraRotationX = 0f;
    private float curCameraRotationX = 0f;
    [SerializeField]
    private float cameraRotationLimit = 85f;

	void Awake () {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
	}
	
	void FixedUpdate () {
        if (!ARMenu.activeSelf)
        {
            PerformMovement();
            PerformRotation();

            if (playerController.IsGrounded()) PerformJump();
        }
    }

    public void Move(Vector3 _velocity)
    {
       velocity = _velocity;
    }

    public void Sprinting(Vector3 _sprintVelocity)
    {
        sprintVelocity = _sprintVelocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void Jump(float _jumpHeight)
    {
        jumpHeight = _jumpHeight;
    }

    public void CameraRotate (float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    void PerformMovement()
    {
        if(Input.GetKey(KeyCode.LeftShift) & sprintVelocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + sprintVelocity * Time.fixedDeltaTime);
        }

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

    void PerformJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
}
