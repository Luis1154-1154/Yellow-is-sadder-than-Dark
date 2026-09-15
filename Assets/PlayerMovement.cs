using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 4f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private float gravity = -9.81f;
    private Vector3 velocity;

    void Start()
    {
        if (controller == null) controller = GetComponent<CharacterController>();
        if (cam == null && Camera.main != null) cam = Camera.main.transform;
    }

    void Update()
    {
        // Aplicar gravedad
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Stick Izquierdo / Teclas WASD
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Calcula la dirección según a dónde esté mirando la cámara
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Gira al personaje hacia la dirección de marcha
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}