using UnityEngine;

public class Object3D : MonoBehaviour
{
    public float speed = 5f;
    void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);

        // Destroy when off screen
        if (transform.position.z < -15f)
        {
            Destroy(gameObject);
        }
    }
}
