using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private float verticalVelocity;
    private float xRotation = 0f;
public AudioSource footstepAudio;
public AudioClip footstepClip;
public float footstepInterval = 0.5f;

private float footstepTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        RotateView();
        Move();
    }

    void Move()
    {
        if (InputBlocker.isBlocked) return;

        Vector3 direction = transform.right * Input.GetAxis("Horizontal") +
                            transform.forward * Input.GetAxis("Vertical");

        direction.Normalize();

        verticalVelocity += gravity * Time.deltaTime;
        direction.y = verticalVelocity;

        controller.Move(direction * moveSpeed * Time.deltaTime);

        if (controller.isGrounded)
            verticalVelocity = 0f;
            bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

if (controller.isGrounded && isMoving)
{
    footstepTimer += Time.deltaTime;
    if (footstepTimer >= footstepInterval)
    {
        footstepAudio.PlayOneShot(footstepClip);
        footstepTimer = 0f;
    }
}
else
{
    footstepTimer = footstepInterval;
}

    }

    void RotateView()
    {
        float mouseX = InputBlocker.isBlocked ? 0f : Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = InputBlocker.isBlocked ? 0f : Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

}
