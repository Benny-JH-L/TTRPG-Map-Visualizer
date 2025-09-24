
using UnityEngine;

public class HexMetrics
{
    private static string _debugStart = "HexMetrics | ";
    public float outerRadius;
    public float innerRadius;

    private HexCell referenceCell;

    public HexMetrics(HexCell cell)
    {
        referenceCell = cell;

        // these are some magic numbers! -> need a way to find these arbitrarily
        outerRadius = 10f;
        innerRadius = outerRadius * 0.866025404f;   // outerRadius * (Mathf.Sqrt(3f) / 2f)

        // idk why it these don't work when inside Map they worked, does the `cell` need a rigid body?
        //(innerRadius, outerRadius) = GetHexRadii(referenceCell);  // when i pass in my hexagon?
        //Debug.Log($"{_debugStart} inner = {innerRadius}, outer = {outerRadius} \n inner3 = {GetInnerHexRadius(referenceCell)}");
    }

    private float GetInnerHexRadius(HexCell cell) // gets inner radius
    {
        MeshCollider meshCollider = cell.GetComponent<MeshCollider>();
        if (meshCollider == null)
            return 1f;

        Bounds bounds = meshCollider.bounds;

        // For a regular hexagon: radius = width / 2
        // But we can also calculate from height: radius = height / sqrt(3)
        float radiusFromWidth = bounds.size.x / 2f;
        float radiusFromHeight = bounds.size.z / Mathf.Sqrt(3f); // assuming Z is the other horizontal axis
        Debug.Log($"{_debugStart}radiusFromWidth = {radiusFromWidth}, radiusFromHeight = {radiusFromHeight}");

        // Use the width-based calculation as it's more reliable
        return radiusFromWidth;
    }

    private (float inner, float outer) GetHexRadii(HexCell cell)
    {
        MeshCollider meshCollider = cell.GetComponent<MeshCollider>();
        if (meshCollider == null)
            return (1f, 1f * 2f / Mathf.Sqrt(3f));

        Bounds bounds = meshCollider.bounds;

        // For a regular hexagon: inner radius = width / 2
        // But we can also calculate from height: inner radius = height / sqrt(3)
        float innerRadiusFromWidth = bounds.size.x / 2f;
        float innerRadiusFromHeight = bounds.size.z / Mathf.Sqrt(3f); // assuming Z is the other horizontal axis
        Debug.Log($"{_debugStart}innerRadiusFromWidth = {innerRadiusFromWidth}, innerRadiusFromHeight = {innerRadiusFromHeight}");

        // Use the width-based calculation as it's more reliable
        float innerRadius = innerRadiusFromWidth;

        // Calculate outer radius from inner radius
        float outerRadius = innerRadius * 2f / Mathf.Sqrt(3f);
        Debug.Log($"{_debugStart}2 inner = {innerRadius}, outer = {outerRadius}");

        return (innerRadius, outerRadius);
    }
}
