using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnergyBiscuit : MonoBehaviour
{
    // movement speed (negative = left)
    public float moveSpeed = -3f;

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);

        // optional safety destroy if far off-screen
        if (transform.position.x < -30f) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc == null) return;

        float max = pc.maxEnergy;
        float current = pc.GetCurrentEnergy();
        float percent = (max <= 0f) ? 0f : current / max;

        if (percent <= 0.30f)
        {
            float target = 1.0f * max;
            pc.AddEnergy(target - current);
        }
        else if (percent > 0.30f && percent < 0.75f)
        {
            pc.AddEnergy(0.25f * max);
        }
        else
        {
            pc.AddEnergy(max - current);
        }

        Destroy(gameObject);
    }
}