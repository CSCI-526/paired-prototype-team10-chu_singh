using UnityEngine;
public class BiscuitSpawner : MonoBehaviour
{
    public GameObject biscuitPrefab;
    public float spawnInterval = 2f;
    public Renderer backgroundRenderer;
    public bool useBackgroundBounds = true;
    public float spawnOffset = 0.5f;
    public float verticalPadding = 0.2f;
    public float spawnViewportX = 1.1f;         // used if no backgroundRenderer or useBackgroundBounds=false
    public float spawnViewportYMin = 0.15f;
    public float spawnViewportYMax = 0.85f;

    GameObject runtimeTemplate;

    Camera cam;
    float timer;


    void Update()
    {
        if ((biscuitPrefab == null) && (runtimeTemplate == null)) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnBiscuit();
        }
    }

    void SpawnBiscuit()
    {
        Vector3 spawnPos;

        if (useBackgroundBounds && backgroundRenderer != null)
        {
            // Use background world bounds
            Bounds b = backgroundRenderer.bounds;

            // X: spawn right of background max x plus offset
            float spawnX = b.max.x + spawnOffset;

            // Y: random inside background's vertical range respecting padding
            float minY = b.min.y + verticalPadding;
            float maxY = b.max.y - verticalPadding;
            if (minY > maxY) // guard
            {
                minY = b.min.y;
                maxY = b.max.y;
            }

            float spawnY = Random.Range(minY, maxY);
            spawnPos = new Vector3(spawnX, spawnY, 0f);
        }
        else
        {
            // Fallback: use camera viewport to spawn slightly off-screen to the right
            float vY = Random.Range(spawnViewportYMin, spawnViewportYMax);
            Vector3 viewportPos = new Vector3(spawnViewportX, vY, Mathf.Abs(cam.transform.position.z));
            Vector3 worldPos = cam.ViewportToWorldPoint(viewportPos);
            worldPos.z = 0f;
            spawnPos = worldPos;
        }

        GameObject instance;
        if (runtimeTemplate != null)
        {
            instance = Instantiate(runtimeTemplate, spawnPos, Quaternion.identity);
            instance.SetActive(true); // runtime template was inactive
        }
        else
        {
            instance = Instantiate(biscuitPrefab, spawnPos, Quaternion.identity);
        }

    }
    void OnDestroy()
    {
        if (runtimeTemplate != null) Destroy(runtimeTemplate);
    }
}