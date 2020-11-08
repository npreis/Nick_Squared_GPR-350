using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointForceGenerator2D : ForceGenerator2D
{
    public Vector2 startingPoint;
    public float startingMagnitude;
    public float startingRange;
    private Vector2 point;
    private float magnitude;
    private float range = 1000;

    // Start is called before the first frame update
    void Start()
    {
        point = startingPoint;
        magnitude = startingMagnitude;
        range = startingRange;
        shouldEffectAll = true;
    }


    public override void UpdateForce(ref PhysicsData2D pData, double dt)
    {
        Vector2 diff = point - pData.pos;
        if (diff.magnitude < range)
        {
            float dist = diff.magnitude;
            float proportionAway = dist / range;
            proportionAway = 1 - proportionAway;
            diff.Normalize();

            pData.accumulatedForces += diff * magnitude * proportionAway;
        }
    }

    public void setPoint(Vector2 point)
    {
        this.point = point;
    }
}
