using UnityEngine;

public class InfiniteEnergy2D : Object2D
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.GrantInfiniteEnergy(5f); // Grant 5 seconds of infinite energy
            Destroy(gameObject);
        }
    }
}
