
using System.Runtime.InteropServices;
using UnityEngine;

public static class Extensions
{

    private static LayerMask layerMask = LayerMask.GetMask("Default");

    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction) 
    {
        if (rigidbody.isKinematic) {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    public static bool DotTest(this Transform transform, Vector2 other, Vector2 testDirection) 
    {
        Vector2 position = new Vector2(transform.position.x,transform.position.y);
        Vector2 direction = other - position;
        
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    } 

}
