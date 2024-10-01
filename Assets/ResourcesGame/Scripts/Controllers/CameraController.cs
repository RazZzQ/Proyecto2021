using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float lookSpeed = 2f; // Sensibilidad de la cámara
    public Transform cameraTransform; // La cámara que sigue al jugador

    private float rotationX = 0f;

    void Start()
    {
        // Bloquea el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Control de la cámara con el ratón
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limita la rotación vertical

        // Rotar la cámara en el eje X (vertical)
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Rotar el personaje en el eje Y (horizontal)
        transform.Rotate(Vector3.up * mouseX);
    }
}