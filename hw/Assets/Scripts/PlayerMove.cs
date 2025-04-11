using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 1f;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float moveRight;
    [SerializeField] private float moveForward;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private bool onGround = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveRight = Input.GetAxis("Horizontal") * (Input.GetKey(KeyCode.LeftShift) ? 10 : 1) * speed;
        moveForward = Input.GetAxis("Vertical") * (Input.GetKey(KeyCode.LeftShift) ? 10 : 1) * speed;
        
        if (Input.GetKeyDown(KeyCode.Space) && onGround) {
            rigidbody.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            // GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled;
        }
    }

    private void FixedUpdate() {
        rigidbody.linearVelocity = new Vector3(
            moveRight,
            rigidbody.linearVelocity.y,
            moveForward
        );
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ground")) {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Ground")) {
            onGround = false;
        }
    }
}
