using UnityEngine;

public class RangeVisualisation : MonoBehaviour
{

    [SerializeField] private Material turretRangeMaterial;

    public void DrawCircle(GameObject container, float radius, float lineWidth)
    {
        var segments = 360;
        var line = container.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;
        line.material = turretRangeMaterial;

        var pointCount = segments + 1;
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, -1);
        }

        line.SetPositions(points);
    }

    public void EraseCircle(GameObject container)
    {
        Destroy(container.GetComponent<LineRenderer>());
    }

}
