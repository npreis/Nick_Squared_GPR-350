using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouyancyForceGenerator2D : ForceGenerator2D
{
    public Particle2D startingID1;
    public float startingObjectVolume;
    public float startingMaxDepth;
    public float startingLiquidPlaneY;
    public float startingLiquidDensity;

    Particle2D id1;
    float objectVolume;
    float maxDepth;
    float liquidPlaneY;
    float liquidDensity;

    // Start is called before the first frame update
    void Start()
    {
        id1 = startingID1;
        objectVolume = startingObjectVolume;
        maxDepth = startingMaxDepth;
        liquidPlaneY = startingLiquidPlaneY;
        liquidDensity = startingLiquidDensity;
        shouldEffectAll = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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

        Vector2 force = new Vector2(0.0f, -Mathf.Abs(yForce));
        obj.accumulatedForces += force;

        id1.mpPhysicsData = obj;
    }
}
