using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouyancyForceGenerator2D : ForceGenerator2D
{
    Particle2D id1;
    float objectVolume;
    float maxDepth;
    float liquidPlaneY;
    float liquidDensity;

    public BouyancyForceGenerator2D(Particle2D startingID1, float startingObjectVolume, float startingMaxDepth, float startingLiquidPlaneY, float startingLiquidDensity)
    {
        id1 = startingID1;
        objectVolume = startingObjectVolume;
        maxDepth = startingMaxDepth;
        liquidPlaneY = startingLiquidPlaneY;
        liquidDensity = startingLiquidDensity;
        shouldEffectAll = true;
    }

    public override void UpdateForce(ref PhysicsDataPtr pData, float dt)
    {
        //TODO LINK UP TO ID THROUGH STATIC CLASS
        PhysicsDataPtr obj = id1.mpPhysicsData;

        float yPos = obj.pos.y;
        float yForce = 0;

        if (yPos >= liquidPlaneY - maxDepth)
            return;
        else if (yPos <= liquidPlaneY - maxDepth)
            yForce = objectVolume * liquidDensity;
        else
            yForce = liquidDensity * objectVolume * (yPos - maxDepth - liquidPlaneY) / (2 * maxDepth);

        Vector2 force = new Vector2(0.0f, Mathf.Abs(yForce));
        obj.accumulatedForces += force;

        id1.mpPhysicsData = obj;
    }
}
