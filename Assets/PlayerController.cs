using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //If you create a SerializeField, it appears in the Unity UI to be altered there, if needed :)
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] bool lockCursor = true;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float gravityAcc = -9.81f;

    float cameraPitch = 0.0f;
    float velocityY;

    CharacterController controller = null;

    // Bool for animations
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        velocityY = 0.0f;
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

    }

    // Update of Mouse Look logic (Changing Pitch of the Camera)
    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        cameraPitch -= mouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);
    }

    // Update for the player's movement using WASD and jump using space bar
    void UpdateMovement()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();
        Vector3 movementVelocity = (transform.forward * inputDir.y + transform.right * inputDir.x) * walkSpeed;

        // update bool to change animation
        if (inputDir.magnitude > 1)
        {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        //Only allows the user to jump if it is grounded
        if (controller.isGrounded)
        {
            velocityY = 0;
            if (Input.GetKeyDown("space"))
            {
                velocityY += jumpSpeed;
            }
        }else
        {
            velocityY += gravityAcc * Time.deltaTime;
        }
        movementVelocity.y = velocityY;
        controller.Move(movementVelocity * Time.deltaTime);
    }

}
