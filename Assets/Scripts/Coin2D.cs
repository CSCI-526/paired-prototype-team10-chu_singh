using UnityEngine;

public class Coin2d : Object2D
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Coin Collected!");

            // Add score
            GameManager.instance.AddScore(value);

            // Destroy coin
            Destroy(gameObject);
        }
    }
}
