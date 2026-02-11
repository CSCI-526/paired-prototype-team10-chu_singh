using UnityEngine;

/// <summary>
/// Simple sprite-based health/energy bar. Assumes the Fill sprite pivot is set to Left.
/// Attach this to the Player GameObject and assign the Fill Transform & SpriteRenderer.
/// </summary>
public class HealthBar : MonoBehaviour
{
    [Tooltip("Drag the Fill child transform (green bar) here.")]
    public Transform fillTransform;

    [Tooltip("Optional: the SpriteRenderer of the fill to lerp color.")]
    public SpriteRenderer fillRenderer;

    [Tooltip("Color at full energy.")]
    public Color fullColor = Color.green;

    [Tooltip("Color at low energy.")]
    public Color lowColor = Color.red;

    float fullLocalScaleX = 1f;
    Vector3 originalLocalScale;

    void Awake()
    {
        if (fillTransform == null)
        {
            Debug.LogError("[HealthBar] fillTransform not assigned on " + gameObject.name);
            return;
        }

        originalLocalScale = fillTransform.localScale;
        fullLocalScaleX = originalLocalScale.x;
    }

    /// <summary>
    /// Set current energy using current and max values (0..max).
    /// </summary>
    public void SetEnergy(float current, float max)
    {
        float fraction = (max <= 0f) ? 0f : Mathf.Clamp01(current / max);
        UpdateVisual(fraction);
    }

    void UpdateVisual(float fraction)
    {
        if (fillTransform == null) return;

        Vector3 s = fillTransform.localScale;
        s.x = Mathf.Max(0.0001f, fullLocalScaleX * fraction);
        fillTransform.localScale = s;

        if (fillRenderer != null)
            fillRenderer.color = Color.Lerp(lowColor, fullColor, fraction);
    }
}