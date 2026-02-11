using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject coin;
    public GameObject obstacle;
    public float spawnInterval = 1f;

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
        // Randomly decide to spawn a coin or an obstacle
        if (Random.value < 0.5f)
        {
            Instantiate(coin, spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(obstacle, spawnPosition, Quaternion.identity);
        }
    }
}
