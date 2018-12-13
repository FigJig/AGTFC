using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [Header("Movement Settings")]
    public float speed;
    public float sprintSpeed;
    public float lookSmoothing;
    public float lookSensitivity;
    public float jumpHeight;

    private float smoothYRot;
    private float smoothXRot;
    
    [SerializeField]
    private float distanceToGround;
    private PlayerMotor motor;

    public LayerMask GroundLayer;

    void Start () {
        motor = GetComponent<PlayerMotor>();
    }
	
	void Update () {

        //Movement
        float _xMovement = Input.GetAxisRaw("Horizontal");
        float _zMovement = Input.GetAxisRaw("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;

        Vector3 _velocity = (_movementHorizontal + _movementVertical).normalized * speed;
        Vector3 _sprintVelocity = (_movementHorizontal + _movementVertical).normalized * sprintSpeed;

        motor.Move(_velocity);
        motor.Sprinting(_sprintVelocity);

        //Jumping
        motor.Jump(jumpHeight);

        //Rotation of character
        float _yRot = Input.GetAxisRaw("Mouse X");
        smoothYRot = Mathf.Lerp(smoothYRot, _yRot, 1f / lookSmoothing);
        Vector3 _rotation = new Vector3(0f, smoothYRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

        //Rotation of camera
        float _xRot = Input.GetAxisRaw("Mouse Y");
        smoothXRot = Mathf.Lerp(smoothXRot, _xRot, 1f / lookSmoothing);
        float _camRotX = smoothXRot * lookSensitivity;

        motor.CameraRotate(_camRotX);
    }

    //Bool to check if the character is grounded, for if we implement a jump
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.01f, GroundLayer);
    }
}
