using UnityEngine;

public static class MyBoundsExtensions
{
    public static Vector3 RandomPositionInBounds(
        this Bounds bounds, 
        float xInnerMargin = 0f, 
        float yInnerMargin = 0f, 
        float zInnerMargin = 0f,
        bool useX = true,
        bool useY = true,
        bool useZ = true)
    {
        Vector3 randomPos = new Vector3(
            Random.Range(bounds.min.x + xInnerMargin, bounds.max.x - xInnerMargin),
            Random.Range(bounds.min.y + yInnerMargin, bounds.max.y - yInnerMargin),
            Random.Range(bounds.min.z + zInnerMargin, bounds.max.z - zInnerMargin));

        if (!useX) randomPos.x = 0f;
        if (!useY) randomPos.y = 0f;
        if (!useZ) randomPos.z = 0f;

        return randomPos;
    }
}
