using UnityEngine;

public class Object2D : MonoBehaviour
{
    public float speed = 5f;
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy when off screen
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
