using UnityEngine;

public class NewMonoBehaviourScript1 : Object2D
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.GrantInvincibility(5f); // Grant 5 seconds of invincibility
            Destroy(gameObject);
        }
    }
}
