using UnityEngine;

public class MovePlayerBehaviour : MonoBehaviour
{
    public float velocity = 3f;
    public float jumpForce = 12f;
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        // MOVIMIENTO
        Vector3 movement = transform.forward * inputV;
        movement *= velocity * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // ROTACIÓN
        if (Mathf.Abs(inputH) > 0.01f)
        {
            float rotationSpeed = 120f;
            Quaternion rotation = Quaternion.Euler(
                0f,
                inputH * rotationSpeed * Time.fixedDeltaTime,
                0f
            );
            rb.MoveRotation(rb.rotation * rotation);
        }

        // ANIMACIÓN: Blend Tree
        animator.SetFloat("MoveZ", inputV);

        // SALTO
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
