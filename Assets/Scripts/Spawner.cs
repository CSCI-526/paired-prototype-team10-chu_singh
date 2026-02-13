using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject coin;
    public GameObject obstacle;
    public GameObject healthPowerup;
    public GameObject energyPowerup;

    public float obstacleStartInterval = 0.5f;
    public float obstacleMinInterval = 0.15f;
    public float rampDuration = 120f;
    public float otherSpawnInterval = 3f;

    public float minY = -4f;
    public float maxY = 4f;
    public float spawnX = 10f;
    public float spawnCheckRadius = 0.8f;
    public LayerMask spawnBlockLayer;
    float obstacleTimer = 0f;
    float otherTimer = 0f;
    float elapsedTime = 0f;
    void Update()
    {
        float dt = Time.deltaTime;
        elapsedTime += dt;

        // Gradually reduce obstacle interval
        float t = Mathf.Clamp01(elapsedTime / rampDuration);
        float currentObstacleInterval = Mathf.Lerp(obstacleStartInterval, obstacleMinInterval, t);

        obstacleTimer += dt;
        if (obstacleTimer >= currentObstacleInterval)
        {
            TrySpawn(obstacle);
            obstacleTimer = 0f;
        }

        otherTimer += dt;
        if (otherTimer >= otherSpawnInterval)
        {
            SpawnOther();
            otherTimer = 0f;
        }
    }

    void SpawnOther()
    {
        float r = Random.value;

        if (r < 0.15f)
            TrySpawn(healthPowerup);
        else if (r < 0.30f)
            TrySpawn(energyPowerup);
        else
            TrySpawn(coin);
    }

    void TrySpawn(GameObject prefab)
    {
        if (prefab == null) return;

        float spawnY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        Collider2D hit = Physics2D.OverlapCircle(spawnPosition, spawnCheckRadius, spawnBlockLayer);

        if (hit == null)
        {
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}