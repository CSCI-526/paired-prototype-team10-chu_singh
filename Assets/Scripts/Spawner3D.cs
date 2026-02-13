using UnityEngine;

public class Spawner3D : MonoBehaviour
{
    public GameObject coin;
    public GameObject obstacle;
    public float spawnInterval = 1f;

    public float minY = -4f;
    public float maxY = 4f;
    public float minX = -4f;
    public float maxX = 4f;

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
        float spawnX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new(spawnX, spawnY, 30f);
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