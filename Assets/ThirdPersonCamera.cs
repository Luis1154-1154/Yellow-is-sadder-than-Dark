using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Arrastra a Jammy aquí
    public Vector3 offset = new Vector3(0f, 1.5f, -3.5f); // Distancia detrás del personaje
    public float sensitivity = 3f; // Sensibilidad del mouse/mando

    private float currentX = 0f;
    private float currentY = 15f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Oculta el ratón en pantalla
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Lee la palanca derecha del mando o el movimiento del mouse
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;
        currentY = Mathf.Clamp(currentY, -10f, 60f); // Limita el ángulo vertical para no traspasar el suelo

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}