using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;

    [Header("Position")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("Smoothing")]
    public float positionSmoothTime = 0.3f;
    public float rotationSmoothSpeed = 3f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (!target) return;

        // --- Smooth Position ---
        Vector3 desiredPosition = target.TransformPoint(offset);
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            positionSmoothTime
        );

        // --- Smooth Rotation ---
        Quaternion desiredRotation = Quaternion.LookRotation(
            target.position - transform.position,
            target.up
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            rotationSmoothSpeed * Time.deltaTime
        );
    }
}
