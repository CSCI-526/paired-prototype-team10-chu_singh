using UnityEngine;
public class HealthBar : MonoBehaviour
{
    public Transform fillTransform;
    public SpriteRenderer fillRenderer;
    public Color fullColor = Color.green;
    public Color lowColor = Color.red;
    float fullLocalScaleX = 1f;
    Vector3 originalLocalScale;
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