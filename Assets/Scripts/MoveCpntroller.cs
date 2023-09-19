using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveController : MonoBehaviour
{
    public LayerMask Obstacle;

    private CharacterController characterController;
    private ActionBasedContinuousMoveProvider moveProvider;

    void Start()
    {
        moveProvider = GetComponentInParent<ActionBasedContinuousMoveProvider>();
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveProvider != null ? moveProvider.moveSpeed : 1f;

        float raycastDistance = 1f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection.normalized, out hit, raycastDistance, Obstacle))
        {
            moveDirection = Vector3.zero;
        }

        characterController.Move(moveDirection * Time.fixedDeltaTime);
    }
}