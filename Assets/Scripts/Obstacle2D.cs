using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle2D : Object2D
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Game Over!");

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
