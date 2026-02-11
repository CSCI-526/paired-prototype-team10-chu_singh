using UnityEngine;

/// <summary>
/// Robust Biscuit spawner that restricts spawn area to a background's world bounds.
/// - If backgroundRenderer is assigned, spawn Y will be clamped to its bounds (with padding).
/// - Spawn X is placed at backgroundBounds.max.x + spawnOffset (so they appear from the right).
/// - Tolerant of scene-instance assigned as biscuitPrefab: creates an internal runtime template if needed.
/// </summary>
public class BiscuitSpawner : MonoBehaviour
{
    [Header("Prefab (assign from Project preferably)")]
    [Tooltip("Assign the EnergyBiscuit prefab asset from the Project window. If you assign a scene object, spawner will clone it safely.")]
    public GameObject biscuitPrefab;

    [Header("Spawn timing")]
    public float spawnInterval = 2f;

    [Header("Background bounds (optional)")]
    [Tooltip("Optional: assign the SpriteRenderer (or any Renderer) of the background that defines the visible area.")]
    public Renderer backgroundRenderer;
    [Tooltip("If true, spawn only while a background renderer is assigned. If false, spawns relative to Camera viewport.")]
    public bool useBackgroundBounds = true;

    [Header("Spawn area & padding")]
    [Tooltip("How far off the right edge of the background to spawn (world units).")]
    public float spawnOffset = 0.5f;
    [Tooltip("Padding from top/bottom edges inside which biscuits can spawn (world units).")]
    public float verticalPadding = 0.2f;

    [Header("Fallback (camera-based)")]
    public float spawnViewportX = 1.1f;         // used if no backgroundRenderer or useBackgroundBounds=false
    public float spawnViewportYMin = 0.15f;
    public float spawnViewportYMax = 0.85f;

    // Runtime protection template (used if user assigned a scene instance)
    GameObject runtimeTemplate;

    Camera cam;
    float timer;

    void Awake()
    {
        cam = Camera.main;
        if (cam == null) Debug.LogWarning("[BiscuitSpawner] Camera.main not found.");

        if (biscuitPrefab == null)
        {
            Debug.LogWarning("[BiscuitSpawner] biscuitPrefab is null in inspector — assign prefab from Project (recommended).");
            return;
        }

        // If the assigned biscuitPrefab is a scene instance, create a hidden runtime template clone.
        // biscuitPrefab.scene.IsValid() == true indicates a scene object (not an asset).
        if (biscuitPrefab.scene.IsValid())
        {
            Debug.LogWarning("[BiscuitSpawner] You assigned a scene instance as biscuitPrefab. Spawner will create an internal template so spawning continues when instances are destroyed. Recommended: create and assign a prefab asset from Project.");
            runtimeTemplate = Instantiate(biscuitPrefab);
            runtimeTemplate.name = biscuitPrefab.name + "_RuntimeTemplate";
            runtimeTemplate.SetActive(false);
            runtimeTemplate.transform.SetParent(transform, true);
        }
    }

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

        // That's it — the biscuit prefab should handle its own movement and collision.
    }

    // Optional: draw debug gizmos for spawn area in editor
    void OnDrawGizmosSelected()
    {
        if (useBackgroundBounds && backgroundRenderer != null)
        {
            Gizmos.color = Color.cyan;
            Bounds b = backgroundRenderer.bounds;
            // draw background bounds
            Gizmos.DrawWireCube(b.center, b.size);

            // draw spawn line to the right
            Vector3 left = new Vector3(b.max.x + spawnOffset, b.min.y + verticalPadding, 0f);
            Vector3 right = new Vector3(b.max.x + spawnOffset, b.max.y - verticalPadding, 0f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(left, right);
        }
    }

    void OnDestroy()
    {
        if (runtimeTemplate != null) Destroy(runtimeTemplate);
    }
}