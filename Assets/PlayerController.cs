using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 50f;
    [SerializeField] bool lockCursor = true;
    [SerializeField] float walkSpeed = 10.0f;

    CharacterController controller = null;

    float cameraPitch = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
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

    // Update for the player's movement using WASD
    void UpdateMovement()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();
        Vector3 velocity = (transform.forward * inputDir.y + transform.right * inputDir.x) * walkSpeed;
        controller.Move(velocity * Time.deltaTime);
    }
}
