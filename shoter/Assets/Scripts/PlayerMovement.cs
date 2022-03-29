using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    PhotonView pv;

    public float moveSpeed = 10;
    float v;
    float h;
    public Vector3 move;

    //sprint
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    public float jumpForce = 6;
    public float groundCheckDistance;
    public bool isGrounded;

    public Animator character;
    //public Animation walking;

    public float mouseSensitivity = 200f;
    float xRotation = 0f;
    public Transform cam;
    public Transform torso;


    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (pv.IsMine)
        {
            cam = GetComponentInChildren<Camera>().transform;
            cam.GetComponent<Camera>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        if (pv.IsMine)
        {
            Jump();
            RotateCamera();
            ControlSpeed();
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        //character.SetFloat("Blend", cam.localRotation.x);
    }

    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            Move();
            if (move.z != 0.0f || move.x != 0.0f)
            {
                character.SetBool("Walking", true);
            }
            else
            {
                character.SetBool("Walking", false);
            }
        }
    }

    void Move()
    {
        move.x = 0f;
        move.z = 0f;

        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        move.x = h;
        move.z = v;

        GetComponent<Transform>().Translate(move * moveSpeed * Time.deltaTime);        
    }
    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);          
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    public void Jump()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
        }
    }
}