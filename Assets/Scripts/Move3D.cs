using UnityEngine;

public class Move3D : MonoBehaviour
{
    public float speed = 1f;
    public float pitchSpeed = 200f;
    public float yawSpeed = 200f;

    private float currentYaw;
    private float currentPitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentYaw = transform.eulerAngles.y;
        currentPitch = transform.eulerAngles.x;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Update rotation values
        currentYaw += mouseX * yawSpeed * Time.deltaTime;
        currentPitch -= mouseY * pitchSpeed * Time.deltaTime;

        // Clamp pitch to prevent flipping
        currentPitch = Mathf.Clamp(currentPitch, -80f, 80f);

        transform.rotation = Quaternion.Euler(
            currentPitch,
            currentYaw,
            0f
        );

        // Always move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.z = 0f;
        transform.position = pos;
    }
}
