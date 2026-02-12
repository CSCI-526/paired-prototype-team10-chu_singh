using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject coin;
    public GameObject obstacle;
    public GameObject healthPowerup;
    public GameObject energyPowerup;
    public float spawnInterval = 0.5f;

    public float minY = -4f;
    public float maxY = 4f;
    public float spawnX = 10f;

    public float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        float spawnY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new(spawnX, spawnY, 0f);
        float randomValue = Random.value;
        // Randomly decide to spawn a coin or an obstacle
        if (randomValue < 0.2f)
        {
            Instantiate(healthPowerup, spawnPosition, Quaternion.identity);
        }
        else if (randomValue < 0.4f)
        {
            Instantiate(energyPowerup, spawnPosition, Quaternion.identity);
        }
        else if (randomValue < 0.7f)
        {
            Instantiate(coin, spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(obstacle, spawnPosition, Quaternion.identity);
        }
    }
}
