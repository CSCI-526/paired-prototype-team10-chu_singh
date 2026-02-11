using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float maxEnergy = 100f;
    public float startEnergy = 100f;
    public float energyDrainPerSecond = 10f;

    public HealthBar healthBar;   // MUST be assigned in Inspector

    private Rigidbody2D rb;
    private Vector2 input;
    private float currentEnergy;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        currentEnergy = startEnergy;

        if (healthBar != null)
            healthBar.SetEnergy(currentEnergy, maxEnergy);
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        bool isMoving = input.sqrMagnitude > 0.0001f;

        if (isMoving && currentEnergy > 0f)
        {
            currentEnergy -= energyDrainPerSecond * Time.deltaTime;
            currentEnergy = Mathf.Max(0f, currentEnergy);

            if (healthBar != null)
                healthBar.SetEnergy(currentEnergy, maxEnergy);
        }
    }

    void FixedUpdate()
    {
        if (currentEnergy <= 0f)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = input.normalized * moveSpeed;
    }

    // 🔥 THIS METHOD MUST UPDATE THE BAR
    public void AddEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0f, maxEnergy);

        if (healthBar != null)
            healthBar.SetEnergy(currentEnergy, maxEnergy);
    }

    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }
}