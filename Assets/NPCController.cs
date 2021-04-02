using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float gravityAcc = -9.81f;
    float velocityY;
    CharacterController controller = null;
    // Start is called before the first frame update
    void Start()
    {
        velocityY = 0.0f;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNPCMovement();
    }

    void UpdateNPCMovement()
    {
        //Only allows the user to jump if it is grounded
        Vector3 movementVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        if (controller.isGrounded)
        {
            velocityY = 0;
        }
        else
        {
            velocityY += gravityAcc * Time.deltaTime;
        }
        movementVelocity.y = velocityY;
        controller.Move(movementVelocity * Time.deltaTime);
    }
}
